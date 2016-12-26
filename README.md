# Pinyin4Netcore

1. 使用Pinyin4J 的拼音数据库。
2. 如果输入的字符不是汉字，则会抛出异常。

## 环境
.net standard library 1.6

## 用法
```
// 设置拼音输出格式
PinyinOutputFormat format = new PinyinOutputFormat(ToneFormat.WITHOUT_TONE, CaseFormat.LOWERCASE, VCharFormat.WITH_U_UNICODE);
char hanzi = '李';
// 判断是否是汉字
if(PinyinUtil.IsHanzi(hanzi)){
    return;
}
// 取出指定汉字的所有拼音
string[] py = Pinyin4Net.GetPinyin(hanzi);
// 取出指定汉字的所有拼音（经过格式化的）
string[] py = Pinyin4Net.GetPinyin(hanzi, format);
// 取指定汉字的唯一或者第一个拼音
Pinyin4Net.GetUniqueOrFirstPinyin(hanzi);
// 取指定汉字的唯一或者第一个拼音（经过格式化的）
Pinyin4Net.GetUniqueOrFirstPinyinWithFormat(hanzi, format);
// 根据拼音查汉字
string[] hanzi = Pinyin4Net.GetHanzi('li', true);
```

## 更新日志

### 2016-12-26 2.3
添加 .net core 支持