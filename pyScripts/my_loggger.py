import logging

logger = logging.getLogger()
logger.setLevel(logging.DEBUG)
formatter = logging.Formatter('%(asctime)s %(filename)s %(funcName)s [line:%(lineno)d] %(levelname)s %(message)s')

# 创建一个handler，用于输出到控制台
sh = logging.StreamHandler()
sh.setFormatter(formatter)
logger.addHandler(sh)

# 创建一个handler，用于写入日志文件
fh = logging.FileHandler('log.log')
fh.setFormatter(formatter)
logger.addHandler(fh)

# 记录一条日志
logger.debug('debug message')

def get_logger():
    return logger


def log(*args, **kwargs):

    output = ""
    for arg in args:
        output += str(arg) + " "

    for key in kwargs:
        output += "[" + str(key) + "," + str(kwargs[key]) + "] "

    logger.debug(output)