namespace Caliburn.Micro
{
	using System.Windows;

	/// <summary>
	/// A host for action related attached properties.
	/// </summary>
	public static class Action
	{
		static readonly ILog Log = LogManager.GetLog(typeof(Action));

		/// <summary>
		/// A property definition representing the target of an <see cref="ActionMessage"/>.  
		/// The DataContext of the element will be set to this instance.
		/// </summary>
		public static readonly DependencyProperty TargetProperty =
			DependencyProperty.RegisterAttached(
				"Target",
				typeof(object),
				typeof(Action),
				new PropertyMetadata(OnTargetChanged)
				);

		/// <summary>
		/// A property definition representing the target of an <see cref="ActionMessage"/>.  
		/// The DataContext of the element is not set to this instance.
		/// </summary>
		public static readonly DependencyProperty TargetWithoutContextProperty =
			DependencyProperty.RegisterAttached(
				"TargetWithoutContext",
				typeof(object),
				typeof(Action),
				new PropertyMetadata(OnTargetWithoutContextChanged)
				);

		/// <summary>
		/// Sets the target of the <see cref="ActionMessage"/>.
		/// </summary>
		/// <param name="d">The element to attach the target to.</param>
		/// <param name="target">The target for instances of <see cref="ActionMessage"/>.</param>
		public static void SetTarget(DependencyObject d, object target)
		{
			d.SetValue(TargetProperty, target);
		}

		/// <summary>
		/// Gets the target for instances of <see cref="ActionMessage"/>.
		/// </summary>
		/// <param name="d">The element to which the target is attached.</param>
		/// <returns>The target for instances of <see cref="ActionMessage"/></returns>
		public static object GetTarget(DependencyObject d)
		{
			return d.GetValue(TargetProperty);
		}

		/// <summary>
		/// Sets the target of the <see cref="ActionMessage"/>.
		/// </summary>
		/// <param name="d">The element to attach the target to.</param>
		/// <param name="target">The target for instances of <see cref="ActionMessage"/>.</param>
		/// <remarks>The DataContext will not be set.</remarks>
		public static void SetTargetWithoutContext(DependencyObject d, object target)
		{
			d.SetValue(TargetWithoutContextProperty, target);
		}

		/// <summary>
		/// Gets the target for instances of <see cref="ActionMessage"/>.
		/// </summary>
		/// <param name="d">The element to which the target is attached.</param>
		/// <returns>The target for instances of <see cref="ActionMessage"/></returns>
		public static object GetTargetWithoutContext(DependencyObject d)
		{
			return d.GetValue(TargetWithoutContextProperty);
		}

		///<summary>
		/// Checks if the <see cref="ActionMessage"/>-Target was set.
		///</summary>
		///<param name="element">DependencyObject to check</param>
		///<returns>True if Target or TargetWithoutContext was set on <paramref name="element"/></returns>
		public static bool HasTargetSet(DependencyObject element)
		{
			return (GetTarget(element) != null)
			  || (GetTargetWithoutContext(element) != null);
		}

		private static void OnTargetWithoutContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SetTargetCore(e, d, false);
		}

		private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SetTargetCore(e, d, true);
		}

		private static void SetTargetCore(DependencyPropertyChangedEventArgs e, DependencyObject d, bool setContext)
		{
			if (Bootstrapper.IsInDesignMode || e.NewValue == e.OldValue || e.NewValue == null)
				return;

			var target = e.NewValue;
			var containerKey = e.NewValue as string;

			if (containerKey != null)
				target = IoC.GetInstance(null, containerKey);

			if (setContext && d is FrameworkElement)
			{
				Log.Info("Setting DC of {0} to {1}.", d, target);
				((FrameworkElement)d).DataContext = target;
			}

			Log.Info("Attaching message handler {0} to {1}.", target, d);
			Message.SetHandler(d, target);
		}
	}
}