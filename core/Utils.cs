using System.Text;

namespace dmzj_spider.core;

public static class Utils {

    private static readonly HashSet<char> IllegalChars = new() {
        '\\', '/', ':', '*', '?', '"', '<', '>', '|'
    };


    public static string replaceIllegalChar(string path) {
        string fileName = Path.GetFileName(path);
        char[] chars = fileName.ToCharArray();
        for (int i = 0; i < chars.Length; i++) {
            if (IllegalChars.Contains(chars[i])) {
                chars[i] = ' ';
            }
        }

        return Path.Combine(Path.GetDirectoryName(path) ?? "", new string(chars));
    }

}