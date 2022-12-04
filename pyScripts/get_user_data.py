import asyncio
from time import sleep
import time
from bilibili_api import video
import my_tools
import my_loggger
import logging

logger = logging.getLogger()

async def main():

    logger.info('开始获取用户数据')
    # my_loggger.log("开始获取用户信息")

    for u in my_tools.get_users():

        # 获取当前时间戳
        t = time.time()
        logger.info("当前时间: " + time.asctime(time.localtime(t)))

        # 当前用户的 关系
        user_info = await my_tools.get_user_info_and_write_to_file(user = u, current_time = int(t))

        # 遍历bvid列表
        for bvid in user_info['bvids']:
            v = video.Video(bvid=bvid)
            my_tools.write_video_info_to_file(user=u, bvid=bvid, info=await v.get_info(), current_time=int(t))
            sleep(1)
    
    logger.info('获取用户数据完成')


if __name__ == '__main__':
    asyncio.get_event_loop().run_until_complete(main())
