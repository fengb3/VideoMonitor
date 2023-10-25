import logging
import my_tools

logger = logging.getLogger()
logger.setLevel(logging.DEBUG)
formatter_log_file = logging.Formatter(
    '%(asctime)s %(filename)s %(funcName)s [line:%(lineno)d] %(levelname)s %(message)s')
formatter_log_console = logging.Formatter('[%(asctime)s][%(levelname)s] %(message)s')

# 创建一个handler，用于输出到控制台
sh = logging.StreamHandler()
sh.setFormatter(formatter_log_console)
logger.addHandler(sh)

# 创建一个handler，用于写入日志文件
fh = logging.FileHandler(my_tools.get_config()['path_log_file'])
fh.setFormatter(formatter_log_file)
logger.addHandler(fh)

# def get_logger():
#     return logger


# def log(*args, **kwargs):

#     output = ""
#     for arg in args:
#         output += str(arg) + " "

#     for key in kwargs:
#         output += "[" + str(key) + "," + str(kwargs[key]) + "] "

#     logger.debug(output)
