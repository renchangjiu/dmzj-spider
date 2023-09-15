# dmzj-spider
动漫之家爬虫, 为了不影响网站的正常使用, 采用了单线程+限速的模式.

核心api是:
1.  获取漫画信息: https://manhua.idmzj.com/api/v1/comic2/comic/detail?channel=pc&app_name=comic&version=1.0.0&timestamp=1694660906746&uid={uid}&comic_py={comicPy}&page=1&size=50
2. 获取章节信息: https://manhua.idmzj.com/api/v1/comic2/chapter/detail?channel=pc&app_name=comic&version=1.0.0&timestamp=1694660906746&uid={uid}&comic_py={comicPy}&chapter_id={c.chapter_id}


用法:
```shell
$ ./dmzj-spider.exe --help

  -c, --comicPy    Required. The phonetic name of the manga.

  -u, --uid        Required. User id.

  -o, --output     Output directory, default is desktop directory.
```
参数说明:
1. comicPy

    漫画主页url的最后一部分, 如: https://manhua.idmzj.com/wzyxdjm, 则为: wzyxdjm.

2. uid

    可在 [我的首页] -> [个人设置] -> [个人资料] 中获取.   

3. output
    
    输出目录, 默认为桌面.


示例:
    `./dmzj-spider.exe -c wzyxdjm -u 117***832`