> [!IMPORTANT]
> This project is no longer supported. Try [MVP](https://github.com/dancher743/unity-mvp) instead of MVC.

# unity-mvc
Implementation of MVC (Model-View-Controller) architectural pattern via Unity engine.

Getting Started
---
To use MVC we should create _Model_, _View_ and _Controller_, setup relations between these three and setup _messages_ between Controllers.

We recommend to get [example project](https://github.com/dancher743/unity-mvc/tree/master#example) before we started.

Let's get started!

### Create a Model
Implement `IModel` interface to create a _Model_ -

```
public class CubeModel : IModel
{
	  ...
}
```

### Create a View
Implement `IView` interface to create a _View_ -

```
public class CubeView : MonoBehaviour, IView
{
	  ...
}
```

### Create a Controller
Derived from `Controller<TView, TModel>` class and specifies types: `TView` and `TModel` to create a _Controller_.

In our case `TModel` is `CubeModel` and `TView` is `CubeView` -

`CubeController : Controller<CubeView, CubeModel>`

```
public class CubeController : Controller<CubeView, CubeModel>
{
	public CubeController(CubeView cubeView, CubeModel cubeModel) : base(cubeView, cubeModel)
  	{
      		...
  	}
}
```

### Create a Controller instance
When we’re done with _Model_, _View_ and _Controller_ next step is going to be a creation of _Controller_ instance.

To achieve this, use `CreateController<TController>(IView view, IModel model)` method of `ControllerManager` static class -

`ControllerManager.CreateController<CubeController>(cubeView, new CubeModel())`

```
[SerializeField]
private CubeView cubeView;

...

void Start()
{
	cubeController = ControllerManager.CreateController<CubeController>(cubeView, new CubeModel());
}
```

You also able to create _Controller_ instance using classic approuch without `ControllerManager` -

```
var cubeController = new CubeController(cubeView, new CubeModel())
```


### Remove a Controller instance
To remove some _Controller_ from `ControllerManager` use `RemoveController<TController>(TController controller)` method -

```
private void OnDestroy()
{
	ControllerManager.RemoveController(cubeController);
	ControllerManager.RemoveController(uiController);
}
```

**NOTE:** You should pass _Controller_ instance (not generic type) to `RemoveController` method to remove it from `ControllerManager`.

Also you can implement an `ICleareable` interface to make additional clean before _Controller_ will be removed. `Clear()` method will be called by `ControllerManager` automatically.

```
public interface ICleareable
{
	void Clear();
}
```
```
public class CubeController : Controller<CubeView, CubeModel>, ICleareable
{
	public CubeController(CubeView cubeView, CubeModel cubeModel) : base(cubeView, cubeModel)
	{
		view.Clicked += OnViewClicked;
		model.ColorChanged += OnModelColorChanged;
	}

	public void Clear()
	{
		view.Clicked -= OnViewClicked;
    		model.ColorChanged -= OnModelColorChanged;
  	}

  	...
}
```

We're done!

Messaging
---
### Receive a Message
Every `Controller` can message with other controllers.

Implement `IMessageReceivable` interface to make class available for message receiving -

```
public interface IMessageReceivable
{
	  void ReceiveMessage<TMessageData>(TMessageData data) where TMessageData : struct;
}
```
```
public class UIController : Controller<UIView, UIModel>, IMessageReceivable
{
  	...

  	public void ReceiveMessage<TMessageData>(TMessageData data)
  	{
		switch (data)
		{
  			case CubeColorData cubeColorData:
    		  		model.Color = cubeColorData.Color;
				break;
		}
  	}
}
```

Block `case CubeColorData cubeColorData` used here as a way to handle some message.

### Dispatch a Message
To dispatch a _Message_ to some controller use `DispatchMessageTo<TController, TMessageData>(TMessageData data)` method of `ControllerManager` -

`ControllerManager.DispatchMessageTo<UIController, CubeColorData>(new CubeColorData(color))`

```
public class CubeController : Controller<CubeView, CubeModel>, ICleareable
{
  	...

	private void OnModelColorChanged(Color color)
	{
		view.Color = color;

		ControllerManager.DispatchMessageTo<UIController, CubeColorData>(new CubeColorData(color));
	}
}
```

Example
---
Example project with the latest version of the package is available [here](https://github.com/dancher743/unity-mvc/releases/tag/example-project).
