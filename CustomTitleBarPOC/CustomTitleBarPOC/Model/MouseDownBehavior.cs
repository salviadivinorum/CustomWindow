using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace CustomTitleBarPOC.Model
{
	/// <summary>
	/// Represents a behavior that executes a command when the associated UI element is clicked.
	/// </summary>
	class MouseDownBehavior : Behavior<UIElement>
	{
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand), typeof(MouseDownBehavior), new PropertyMetadata(null));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
		}

		private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ICommand command = Command;
			if (command?.CanExecute(e) == true)
			{
				command.Execute(e);
			}
		}
	}
}
