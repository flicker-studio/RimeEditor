# AddressableAssetsData

- 可寻址资产系统在运行时需要一些文件来知道要加载什么以及如何加载它。
这些文件是在生成`Addressables`数据时生成的，并最终位于`StreamingAssets`文件夹中，该文件夹是 Unity 中的一个特殊文件夹，其中包含生成中的所有文件。
当构建`Addressables`内容时，系统会将这些文件暂存到库中，在生成应用程序时，系统会将所需的文件复制到`StreamingAssets`。