# Pinyin4NET
.net环境下使用的拼音-汉字互转库。

支持以下目标版本:
- net4.0
- net4.5
- netcore1.0
- netcore1.1
- netcore2.0
- netstandard1.6
- netstandard2.0

.NET版本对应关系参见 [How to target the .NET Standard](https://docs.microsoft.com/en-us/dotnet/core/tutorials/libraries#how-to-target-the-net-standard)



## 获取源码与发行版

GitHub [zip](https://github.com/hyjiacan/Pinyin4Net/archive/master.zip)
```shell
git clone https://gitee.com/hyjiacan/Pinyin4Net.git
```

码云 [zip](https://gitee.com/hyjiacan/Pinyin4Net/repository/archive/master.zip)
```shell
git clone https://github.com/hyjiacan/Pinyin4Net.git
```

[发行版](https://gitee.com/hyjiacan/Pinyin4Net/attach_files)

## 编译

> 注：开发环境已切换为 [Visual Studio Code](https://code.visualstudio.com/)

执行项目目录下的*build.bat*/*build.sh*可以直接开始生成所有支持的目标的**Release**版本，
若需要生成某个版本，请参考以下命令。

```shell
cd hyjiacan.py4n
```

编译**DEBUG**版本

```shell
dotnet build --configuration Debug
# 或
dotnet build
```
> 注：`--configuration Debug` 为默认配置

编译为**RELEASE**版本
```shell
dotnet build --configuration Release
```

若要编译指定的版本，使用以下参数：

```shell
dotnet build -f net40
```

当前配置可以使用的版本如下：

- NET40
- NET45
- NETCOREAPP1.0
- NETCOREAPP1.1
- NETCOREAPP2.0
- NETSTANDARD1.6
- NETSTANDARD2.0

> 若要编译成其它的目标版本，需要自行修改*hyjiacan.py4n.csptoj*里面的`TargetFrameworks`节点，
> 版本名称见[How to target the .NET Framework](https://docs.microsoft.com/en-us/dotnet/core/tutorials/libraries#how-to-target-the-net-framework)

## 单元测试

> 单元测试需要安装对应的.net版本

```shell
cd UnitTestProject
```

运行所有版本的测试
```shell
dotnet test
```

运行指定版本的测试
```shell
dotnet test -f net40
```

> 注意：.net4.0 或以下版本不支持`MsTest`单元测试

## 接口

> 提供的所有接口，均为静态接口。

### Pinyin4Net 汉字拼音查询

> 汉字查询接口都放在类 **Pinyin4Net** 内

#### 汉字查拼音

```csharp
/// <summary>
/// 获取汉字的拼音数组
/// </summary>
/// <param name="hanzi">要查询拼音的汉字字符</param>
/// <returns>汉字的拼音数组，若未找到汉字拼音，则返回空数组</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string[] GetPinyin(char hanzi)

/// <summary>
/// 获取唯一拼音(单音字)或者第一个拼音(多音字)
/// </summary>
/// <param name="hanzi">要查询拼音的汉字字符</param>
/// <returns>返回唯一拼音(单音字)或者第一个拼音(多音字)</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string GetUniqueOrFirstPinyin(char hanzi)

/// <summary>
/// 获取格式化后的拼音
/// </summary>
/// <param name="hanzi">要查询拼音的汉字字符</param>
/// <param name="format">拼音输出格式化参数</param>
/// <see cref="PinyinOutputFormat"/>
/// <seealso cref="PinyinFormatter"/>
/// <returns>返回经过格式化的拼音</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string[] GetPinyinWithFormat(char hanzi, PinyinOutputFormat format)

/// <summary>
/// 获取格式化后的唯一拼音(单音字)或者第一个拼音(多音字)
/// </summary>
/// <param name="hanzi">要查询拼音的汉字字符</param>
/// <param name="format">拼音输出格式化参数</param>
/// <see cref="PinyinOutputFormat"/>
/// <seealso cref="PinyinFormatter"/>
/// <returns>格式化后的唯一拼音(单音字)或者第一个拼音(多音字)</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string GetUniqueOrFirstPinyinWithFormat(char hanzi, PinyinOutputFormat format)

/// <summary>
/// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式）
/// </summary>
/// <param name="text">要获取拼音的汉字字符串</param>
/// <param name="format">拼音输出格式化参数</param>
/// <param name="caseSpread">是否将前面的格式中的大小写扩展到其它非拼音字符，默认为false。firstLetterOnly为false时有效 </param>
/// <param name="firstLetterOnly">是否只取拼音首字母，为true时，format无效</param>
/// <param name="multiFirstLetter">firstLetterOnly为true时有效，多音字的多个读音首字母是否全取，如果多音字拼音首字母相同，只保留一个</param>
/// <returns>firstLetterOnly为true时，只取拼音首字母格式为[L]，后面追加空格；multiFirstLetter为true时，多音字的多个拼音首字母格式为[L, H]，后面追加空格</returns>
public static string GetPinyin(string text, PinyinOutputFormat format, bool caseSpread, bool firstLetterOnly, bool multiFirstLetter)

/// <summary>
/// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式）
/// </summary>
/// <param name="text">要获取拼音的汉字字符串</param>
/// <param name="format">拼音输出格式化参数</param>
/// <param name="caseSpread">是否将前面的格式中的大小写扩展到其它非拼音字符，默认为false。firstLetterOnly为false时有效 </param>
/// <param name="pinyinHandler">
/// 拼音处理器，在获取到拼音后通过这个来处理，
/// 如果传null，则默认取第一个拼音（多音字），
/// 参数：
/// 1 string[] 拼音数组
/// 2 char 当前的汉字
/// 3 string 要转成拼音的字符串
/// return 拼音字符串，这个返回值将作为这个汉字的拼音放到结果中
/// </param>
/// <returns>firstLetterOnly为true时，只取拼音首字母格式为[L]，后面追加空格；multiFirstLetter为true时，多音字的多个拼音首字母格式为[L, H]，后面追加空格</returns>
public static string GetPinyin(string text, PinyinOutputFormat format, bool caseSpread, Func<string[], char, string, string> pinyinHandler)

/// <summary>
/// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式），format中指定的大小写模式不会扩展到非拼音字符
/// </summary>
/// <param name="text">要获取拼音的汉字字符串</param>
/// <param name="format">拼音输出格式化参数</param>
/// <returns>格式化后的拼音字符串</returns>
public static string GetPinyin(string text, PinyinOutputFormat format)
```

#### 拼音查汉字

```csharp
/// <summary>
/// 根据单个拼音查询匹配的汉字
/// </summary>
/// <param name="pinyin">要查询汉字的单个拼音</param>
/// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符</param>
/// <returns></returns>
public static string[] GetHanzi(string pinyin, bool matchAll)
```

### Pinyin4Name 姓名拼音查询

> 姓名查询接口都放在类 **Pinyin4Name** 内

```csharp
/// <summary>
/// 获取姓的拼音，如果是复姓则由空格分隔
/// </summary>
/// <param name="firstName">要查询拼音的姓</param>
/// <returns>返回姓的拼音，若未找到姓，则返回null</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string GetPinyin(string firstName)

/// <summary>
/// 获取姓的首字母，如果是复姓则由空格分隔首字母
/// </summary>
/// <param name="firstName">要查询拼音的姓</param>
/// <returns>返回姓的拼音首字母，若未找到姓，则返回null</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string GetFirstLetter(string firstName)

/// <summary>
/// 获取格式化后的拼音
/// </summary>
/// <param name="firstName">要查询拼音的姓</param>
/// <param name="format">输出拼音格式化参数</param>
/// <see cref="PinyinOutputFormat"/>
/// <seealso cref="PinyinFormatter"/>
/// <returns>返回格式化后的拼音，若未找到姓，则返回null</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string GetPinyinWithFormat(string firstName, PinyinOutputFormat format)

/// <summary>
/// 根据拼音查询匹配的姓
/// </summary>
/// <param name="pinyin"></param>
/// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符，此参数用于告知传入的拼音是完整拼音还是仅仅是声母</param>
/// <returns>匹配的姓数组</returns>
public static string[] GetHanzi(string pinyin, bool matchAll)
```

### 格式化参数

> 用于对拼音输入进行格式化控制

```csharp
/// <summary>
/// 大小写格式
/// </summary>
public enum CaseFormat
{
    /// <summary>
    /// 首字母大写，此选项对 a e o 几个独音无效
    /// </summary>
    CAPITALIZE_FIRST_LETTER,
    /// <summary>
    /// 全小写
    /// </summary>
    LOWERCASE,
    /// <summary>
    /// 全大写
    /// </summary>
    UPPERCASE
}

/// <summary>
/// 声调格式
/// </summary>
public enum ToneFormat
{
    /// <summary>
    /// 带声调标志
    /// </summary>
    WITH_TONE_MARK,
    /// <summary>
    /// 不带声调
    /// </summary>
    WITHOUT_TONE,
    /// <summary>
    /// 带声调数字值
    /// </summary>
    WITH_TONE_NUMBER
}

/// <summary>
/// V(ü)字符格式
/// </summary>
public enum VCharFormat
{
    /// <summary>
    /// 将 ü 输出为 u:
    /// </summary>
    WITH_U_AND_COLON,
    /// <summary>
    /// 将 ü 输出为 v
    /// </summary>
    WITH_V,
    /// <summary>
    /// 将 ü 输出为ü
    /// </summary>
    WITH_U_UNICODE
}
```

通过实例化输入格式化类 `PinyinOutputFormat`，
设置上面的这些枚举，再将其传给获取拼音的函数，如：`GetPinyinWithFormat` 等，
即可格式化拼音输入。(请看 [示例](#示例))

## 示例

```csharp
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

### 姓名拼音查询

```csharp
string firstName = "单于";
// 取出姓的拼音
string py = Pinyin4Name.GetPinyin(firstName);
// 取出姓的拼音首字母
string py = Pinyin4Name.GetFirstLetter(firstName);
// 取出姓的拼音(格式化后)
string py = Pinyin4Name.GetPinyinWithFormat(firstName, format);
// 取出匹配拼音的姓
string[] firstNames = Pinyin4Name.GetHanzi("li", false);
```
