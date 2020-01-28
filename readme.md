![image](https://user-images.githubusercontent.com/38829279/72772519-16b83a80-3c3f-11ea-8ecd-b29719bc3101.png)

一个简单基于netcore3.1的windows定时关机助手;

可以在指定时间后休眠-关机-重启;

可以设置 时-分-秒;

## 实现细节

基于mvvm, 主要逻辑在 `MainViewModel` 中;

主要视图组件 `TimerControl`, 负责计时, 计时结束时触发`TimingOver` 事件;
`TimerControl` 主要开放的方法:

```CSharp
public void BeginTiming();
public void StopTiming();
```

---
