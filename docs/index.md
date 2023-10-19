 <h1 align="center">

![Logo](resources/images/logo/head.png)

ProJect-Moon
</h1>

> [!NOTE]
> 在开始之前，你需要了解下面列出的基本知识

## 代码样式规则

本项目使用`EditorConfig`来约定代码样式，你可以按照教程在[VS](https://learn.microsoft.com/zh-cn/visualstudio/ide/code-styles-and-code-cleanup?view=vs-2022)和[Rider](https://www.jetbrains.com/help/rider/Using_EditorConfig.html)中轻松配置代码样式并启用智能提醒。

下面是一个符合代码样式的示例：

```c#
  public void Motion()
  {
      foreach (var motionState in m_playerMoveStates)
      {
          motionState.Motion();
      }
  }
```

## Git Commit规范

Commit Message规范如下，参考于[Conventional Commits](<https://www.conventionalcommits.org/en/v1.0.0/#:~:text=feat%3A%20a%20commit%20of%20the,with%20MAJOR%20in%20Semantic%20Versioning>)

```text
<type>(<scope>): <subject>

<body>

<footer>
```

关键字解释如下：

- `type`,*必选项*,commit的类型，各项含义见下表:

|     类型     |                描述                |
|:------------:|:--------------------------------:|
| feat/feature |            新功能的添加            |
|     fix      |             BUG的修复              |
|     docs     |             仅文档更改             |
|    style     |        不影响代码含义的更改        |
|   refactor   | 既不修复错误也不添加功能的代码更改 |
|     perf     |         提高性能的代码更改         |
|     test     |    添加缺少的测试或更正现有测试    |
|    build     |   影响生成系统或外部依赖项的更改   |
|      ci      |      更改的 Cl 配置文件和脚本      |
|    chore     |   不修改代码或测试文件的其他更改   |
|    revert    |           还原以前的提交           |

- `scope`，*可选项*，commit 影响的范围，比如：route，component，utils，build...

- `subject`，*必选项*，commit 的概述.

- `body`，*必选项*，commit 具体修改内容，可以分为多行.

- `footer`，*必选项*，一些备注，通常是[SKIP CI]或Closed #2.

## 代码注释

> [!IMPORTANT]
> 为了生成可读性良好的开发手册，请使用符合C#标准的xml注释！
> 你可以在[微软官方手册](https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/xmldoc/)中看到详细的解释．
> [!NOTE]
> 由于编码问题，中文注释会在转换时随机出现乱码，请尽可能使用英文注释(机翻也行)

## 单元测试

> [!IMPORTANT]
> 单元测试有助于验证程序的健壮性，务必多加注意!

关于单元测试的用法，参见[Unity Unit Test](https://docs.unity3d.com/Packages/com.unity.test-framework@1.3/manual/index.html)

单元测试已配置代码覆盖率信息，你可以通过运行根目录下的`CodeCoverage.bat`批处理程序快速打开生成的覆盖率测试结果.

也可以手动打开位于`moon-dev/CodeCoverage/Report/index.html`的网页查看.

## 开发手册

本项目使用[Docfx](https://github.com/dotnet/docfx)生成开发手册,查看[官方手册](https://dotnet.github.io/docfx/)以快速入门.
