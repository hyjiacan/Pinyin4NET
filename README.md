# Pinyin4NET

.net 环境下使用的拼音-汉字互转库

![Nuget](https://img.shields.io/nuget/v/hyjiacan.pinyin4net?style=flat-square)
![Nuget](https://img.shields.io/nuget/dt/hyjiacan.pinyin4net?style=flat-square)
![GitHub](https://img.shields.io/github/license/hyjiacan/pinyin4net?style=flat-square)

所有的目标版本都在这一个分支上，现支持以下目标版本:

- net4.0
- net4.5
- net4.6
- net4.6.1
- net4.6.2
- net4.7
- net4.8
- net5.0
- net6.0
- netcore2.0
- netcore3.0
- netcore3.1
- netstandard1.6
- netstandard2.0
- netstandard2.1

.NET 版本对应关系参见 [How to target the .NET Standard](https://docs.microsoft.com/en-us/dotnet/core/tutorials/libraries#how-to-target-the-net-standard)

## 源码与发行版

GitHub [zip](https://github.com/hyjiacan/Pinyin4Net/archive/master.zip)

```bash
git clone https://github.com/hyjiacan/Pinyin4Net.git
```

码云 [zip](https://gitee.com/hyjiacan/Pinyin4Net/repository/archive/master.zip)

```bash
git clone https://gitee.com/hyjiacan/Pinyin4Net.git
```

[发行版](https://gitee.com/hyjiacan/Pinyin4Net/attach_files)

## nuget 安装

> 包 ID 在 4.0(含) 以前叫做 `hyjiacan.py4n`

Package Manager

```bash
Install-Package hyjiacan.pinyin4net
```

.NET CLI

```bash
dotnet add package hyjiacan.pinyin4net
```

Packet CLI

```bash
paket add hyjiacan.pinyin4net
```

> 注：nuget 上还有一个 `Pinyin4Net` 的包，
> 那是有网友基于此项目的早期版本发布的，并非本人发布。

## 编译

> 注：开发环境已切换为 [Visual Studio Code](https://code.visualstudio.com/)

执行项目目录下的*build.bat*/*build.sh*可以直接开始生成所有支持的目标的**Release**版本，
若需要生成某个版本，请参考以下命令。

```bash
cd hyjiacan.py4n
```

编译**DEBUG**版本

```bash
dotnet build --configuration Debug
# 或
dotnet build
```

> 注：`--configuration Debug` 为默认配置

编译为**RELEASE**版本

```bash
dotnet build --configuration Release
```

若要编译指定的版本，使用以下参数：

```bash
dotnet build -f net40
```

当前配置可以使用的版本如下：

- NET40
- NET45
- NETCOREAPP2.0
- NETCOREAPP3.0
- NETSTANDARD1.6
- NETSTANDARD2.0
- NETSTANDARD2.1

> 若要编译成其它的目标版本，需要自行修改*hyjiacan.py4n.csptoj*里面的`TargetFrameworks`节点，
> 版本名称见[How to target the .NET Framework](https://docs.microsoft.com/en-us/dotnet/core/tutorials/libraries#how-to-target-the-net-framework)

## 单元测试

> 单元测试需要安装对应的.net 版本

```bash
cd UnitTestProject
```

运行所有版本的测试

```bash
dotnet test
```

运行指定版本的测试

```bash
dotnet test -f net45
```

> 注意：.net4.0 或以下版本不支持`MsTest`单元测试

## WebDemo

> Demo 网站基于**.NETCORE2.0**

```bash
cd WebDemo
dotnet build
dotnet run
```

## 接口

> 提供的所有接口，均为静态接口。

### Pinyin4Net 汉字拼音查询

> 汉字查询接口都放在类 **Pinyin4Net** 内

#### 汉字查拼音

```csharp
/// <summary>
/// 更新拼音数据库
/// </summary>
/// <param name="data">多音字作在数组中</param>
/// <param name="replace">是否替换已经存在的项，默认为 false</param>
public static void UpdateMap(Dictionary<char, string[]> data, bool replace = false);

/// <summary>
/// 获取汉字的拼音数组
/// </summary>
/// <param name="hanzi">要查询拼音的汉字字符</param>
/// <param name="format">设置输出拼音的格式</param>
/// <returns>汉字的拼音数组，若未找到汉字拼音，则返回空数组</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string[] GetPinyin(char hanzi, PinyinFormat format = PinyinFormat.None);

/// <summary>
/// 获取格式化后的唯一拼音(单音字)或者第一个拼音(多音字)
/// </summary>
/// <param name="hanzi">要查询拼音的汉字字符</param>
/// <param name="format">拼音输出格式化参数</param>
/// <see cref="PinyinFormat"/>
/// <seealso cref="PinyinUtil"/>
/// <returns>格式化后的唯一拼音(单音字)或者第一个拼音(多音字)</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string GetFirstPinyin(char hanzi, PinyinFormat format = PinyinFormat.None);
{
    var pinyin = GetPinyin(hanzi)[0];
    if (format == PinyinFormat.None)
    {
        return pinyin;
    }
    return PinyinUtil.Format(pinyin, format);
}

/// <summary>
/// 获取一个字符串内所有汉字的拼音数组
/// </summary>
/// <param name="text">要获取拼音的汉字字符串</param>
/// <param name="format">拼音输出格式化参数</param>
/// <returns>返回拼音列表，每个汉字的拼音会作为一个数组存放（无论是单音字还是多音字）</returns>
/// <see cref="PinyinItem"/>
public static List<PinyinItem> GetPinyinArray(string text, PinyinFormat format);

/// <summary>
/// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式）
/// </summary>
/// <param name="text">要获取拼音的汉字字符串</param>
/// <param name="format">拼音输出格式化参数</param>
/// <param name="caseSpread">是否将前面的格式中的大小写扩展到其它非拼音字符，默认为false。firstLetterOnly为false时有效 </param>
/// <param name="firstLetterOnly">是否只取拼音首字母，为true时，format无效</param>
/// <param name="multiFirstLetter">firstLetterOnly为true时有效，多音字的多个读音首字母是否全取，如果多音字拼音首字母相同，只保留一个</param>
/// <returns>firstLetterOnly为true时，只取拼音首字母格式为[L]，后面追加空格；multiFirstLetter为true时，多音字的多个拼音首字母格式为[L, H]，后面追加空格</returns>
public static string GetPinyin(string text, PinyinFormat format, bool caseSpread, bool firstLetterOnly, bool multiFirstLetter);

/// <summary>
/// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式）
/// </summary>
/// <param name="text">要获取拼音的汉字字符串</param>
/// <param name="format">拼音输出格式化参数</param>
/// <param name="caseSpread">是否将前面的格式中的大小写扩展到其它非拼音字符，默认为false。</param>
/// <param name="pinyinHandler">
/// 拼音处理器，在获取到拼音后通过这个来处理，
/// 如果传null，则默认取第一个拼音（多音字），
/// 参数：
/// 1 string[] 拼音数组
/// 2 char 当前的汉字
/// 3 string 要转成拼音的字符串
/// return 拼音字符串，这个返回值将作为这个汉字的拼音放到结果中
/// </param>
public static string GetPinyin(string text, PinyinFormat format, bool caseSpread, Func<string[], char, string, string> pinyinHandler);

/// <summary>
/// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式），format中指定的大小写模式不会扩展到非拼音字符
/// </summary>
/// <param name="text">要获取拼音的汉字字符串</param>
/// <param name="format">拼音输出格式化参数</param>
/// <returns>格式化后的拼音字符串</returns>
public static string GetPinyin(string text, PinyinFormat format);
```

`PinyinItem`：

这是一个继承了 `List<string>` 的数据结构，包含以下字段:

- `IsHanzi` 标识是否是汉字字符
- `RawChar` 原始的字符

#### 拼音查汉字

```csharp
/// <summary>
/// 根据单个拼音查询匹配的汉字
/// </summary>
/// <param name="pinyin">要查询汉字的单个拼音</param>
/// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符</param>
/// <returns></returns>
public static string[] GetHanzi(string pinyin, bool matchAll);
```

### Name4Net 姓氏拼音查询

> 姓氏查询接口都放在类 **Name4Net** 内


```csharp
/// <summary>
/// 更新姓氏数据库
/// </summary>
/// <param name="data">复姓的拼音使用一个空格分隔</param>
/// <param name="replace">是否替换已经存在的项，默认为 false</param>
public static void UpdateMap(Dictionary<string, string> data, bool replace = false);

/// <summary>
/// 获取姓的拼音，如果是复姓则由空格分隔
/// </summary>
/// <param name="firstName">要查询拼音的姓</param>
/// <param name="format">输出拼音格式化参数</param>
/// <returns>返回姓的拼音，若未找到姓，则返回null</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string GetPinyin(string firstName, PinyinFormat format = PinyinFormat.None);

 /// <summary>
/// 获取姓的首字母，如果是复姓则由空格分隔首字母
/// </summary>
/// <param name="firstName">要查询拼音的姓</param>
/// <returns>返回姓的拼音首字母，若未找到姓，则返回null</returns>
/// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
public static string GetFirstLetter(string firstName);

/// <summary>
/// 根据拼音查询匹配的姓
/// </summary>
/// <param name="pinyin"></param>
/// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符，此参数用于告知传入的拼音是完整拼音还是仅仅是声母</param>
/// <returns>匹配的姓数组</returns>
public static string[] GetHanzi(string pinyin, bool matchAll);
```

### 格式化参数

> 用于对拼音输入进行格式化控制

```csharp
[Flags]
public enum PinyinFormat
{
    /// <summary>
    /// 不指定格式
    /// </summary>
    None,
    /// <summary>
    /// 首字母大写，此选项对 a e o i u 几个独音无效
    /// </summary>
    CAPITALIZE_FIRST_LETTER = 1 << 1,
    /// <summary>
    /// 全小写
    /// </summary>
    LOWERCASE = 1 << 2,
    /// <summary>
    /// 全大写
    /// </summary>
    UPPERCASE = 1 << 3,
    /// <summary>
    /// 将 ü 输出为 u:
    /// </summary>
    WITH_U_AND_COLON = 1 << 4,
    /// <summary>
    /// 将 ü 输出为 v
    /// </summary>
    WITH_V = 1 << 5,
    /// <summary>
    /// 将 ü 输出为 ü
    /// </summary>
    WITH_U_UNICODE = 1 << 6,
    /// <summary>
    /// 将 ü 输出为 yu
    /// </summary>
    WITH_YU = 1 << 10,
    /// <summary>
    /// 带声调标志
    /// </summary>
    WITH_TONE_MARK = 1 << 7,
    /// <summary>
    /// 不带声调
    /// </summary>
    WITHOUT_TONE = 1 << 8,
    /// <summary>
    /// 带声调数字值
    /// </summary>
    WITH_TONE_NUMBER = 1 << 9,
}
```

通过组合位标识值即可格式化拼音输入。(请看 [示例](#示例))

## 示例

```csharp
// 设置拼音输出格式
PinyinFormat format = PinyinFormat.WITHOUT_TONE | PinyinFormat.LOWERCASE | PinyinFormat.WITH_U_UNICODE;
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
Pinyin4Net.GetFirstPinyin(hanzi);
// 取指定汉字的唯一或者第一个拼音（经过格式化的）
Pinyin4Net.GetPinyin(hanzi, format);
// 根据拼音查汉字
string[] hanzi = Pinyin4Net.GetHanzi('li', true);
```

### 姓氏拼音查询

```csharp
string firstName = "单于";
// 取出姓的拼音
string py = Name4Net.GetPinyin(firstName);
// 取出姓的拼音首字母
string py = Name4Net.GetFirstLetter(firstName);
// 取出姓的拼音(格式化后)
string py = Name4Net.GetPinyin(firstName, format);
// 取出匹配拼音的姓
string[] firstNames = Name4Net.GetHanzi("li", false);
```

## 数据源

### 拼音库

完整引用了 [pinyin4j](http://pinyin4j.sourceforge.net/) 的拼音数据库，在此表示感谢 :+1: :+1: :+1:

### 姓氏库

姓氏数据源自 http://fanwen.geren-jianli.org/622876.html，
经本人整理使用。

> 库可能并不完整，或者有些姓氏拼音有误，欢迎通过 [Issues](https://gitee.com/hyjiacan/Pinyin4Net/issues/new) 更正或者直接通过 PR 提交。
>
> 部分姓氏存在多种不同的读音，在库中仅使用了常用的读音。
>
> 也可以使用 `Name4Net.UpdateMap()` 接口自行处理。

## 捐赠列表

> 按捐赠时间先后从上至下排列

- [king.jin](https://gitee.com/kingking)
- 袁智远

感谢以上朋友的支持，你们使开源更有信心。

## 待办

- 给姓氏的 GetPinyi 接口添加自定义的处理器
- 对拼音格式化添加 ǘ -> yu 的支持