import json
import os
import time
from bilibili_api import user
import logging

logger = logging.getLogger()

def get_config():
    '''
    读取配置文件
    '''

    with open('config.json', 'r') as f:
        c = json.load(f)

    return c

def get_users():
    '''
    获取用户列表
    '''
    c = get_config()
    return [user.User(userId) for userId in c['uids']]

def get_user(index=0):
    '''
    获取用户
    '''
    c = get_config()
    u = user.User(c['uids'][index])
    return u


async def get_my_video_bvids(user):
    '''
    获取用户所有视频bvid
    '''

    c = get_config()
    videos = await user.get_videos()
    bvids = []
    for video in videos['list']['vlist']:
        bvids.append(video['bvid'])

    logger.info(str(user.get_uid()) + "\n 获取到bvids: " + bvids)

    return bvids

def write_bvids_to_file(user,bvids):
    '''
    将bvid列表写入文件
    '''
    c = get_config()
    dir = c['path_data'] + "\\" + str(user.get_uid())

    if not os.path.exists(dir):
        os.makedirs(dir)

    path = dir + "\\" + c['surfix_bvids_file']
    with open(path, 'w') as f:
        # for bvid in bvids:
        #     f.write(bvid + '\n')

        bvids = json.dumps(bvids)
        f.write(bvids)

async def get_user_info_and_write_to_file(user, current_time = int(time.time())):
    '''
    获取用户关系信息并写入文件
    '''
    c = get_config()
    videos_info = await user.get_videos()
    releation_info = await user.get_relation_info()
    user_info = await user.get_user_info()
    bvids = [video['bvid'] for video in videos_info['list']['vlist']]

    obj = {
        'user_info': user_info,
        'releation_info': releation_info,
        'bvids': bvids
    }

    logger.info("获取到 " + obj['user_info']['name'] + " 的信息")

    dir = c['path_data'] + "\\" + str(user.get_uid()) +"\\"+c['path_user_info']

    if not os.path.exists(dir):
        os.makedirs(dir)

    path = dir + "\\" + str(current_time) + c['surfix_user_info_file']

    logger.info("写入文件: " + path)
    with open(path,'w') as f:
        f.write(json.dumps(obj, indent=4))

    return obj


def read_bvids_from_file(user):
    '''
    从文件中读取bvid列表
    '''
    c = get_config()

    dir = c['path_data'] + "\\" + str(user.get_uid())

    if not os.path.exists(dir):
        logger.info(dir + " 文件夹不存在, 无法读取bvids")

    path = dir + "\\" + c['surfix_bvids_file']
    with open(path, 'r') as f:
        bvids = f.read()
        bvids = json.loads(bvids)

    return bvids



async def get_video_info(bvid):
    video = video.Video(bvid)
    return await video.get_info()

def write_video_info_to_file(user, bvid, info, current_time = int(time.time())):
    '''
    将视频信息写入文件
    '''
    c = get_config()
    dir = c['path_data'] + "\\" + str(user.get_uid()) + "\\" + bvid

    if not os.path.exists(dir):
        os.makedirs(dir)

    path = dir + "\\" + str(current_time) + c['surfix_video_info_file']

    logger.info("写入文件: "+ path)
    with open(path, 'w') as f:
        info = json.dumps(info, indent=4)
        f.write(info)

def get_current_time():
    return int(time.time())