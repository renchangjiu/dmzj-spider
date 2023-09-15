# dmzj-spider
动漫之家爬虫, 为了不影响网站的正常使用, 采用了单线程+限速的模式.

用法:
```shell
$ ./dmzj-spider.exe --help

  -c, --comicPy    Required. The phonetic name of the manga.

  -u, --uid        Required. User id.

  -o, --output     Output directory, default is desktop directory.
```
参数说明:
1. comicPy

    漫画主页url的最后一部分, 如: https://manhua.idmzj.com/wzyxdjm, 则为: wzyxdjm

2. uid

    可在 [我的首页] -> [个人设置] -> [个人资料] 中获取   

3. output
    
    输出目录, 默认为桌面


示例:
    `./dmzj-spider.exe -c wzyxdjm -u 117***832`