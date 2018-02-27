using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helical_Wheel_App.Behaviors
{
    public class ValidateOnTextChangedBehavior : Behavior<Editor>
    {
        private VisualElement _element;

        public static readonly BindableProperty ValidateCommandProperty =
                BindableProperty.Create("ValidateCommand", typeof(ICommand),
                    typeof(ValidateOnTextChangedBehavior), default(ICommand),
                    BindingMode.OneWay, null);

        public ICommand ValidateCommand
        {
            get { return (ICommand)GetValue(ValidateCommandProperty); }
            set { SetValue(ValidateCommandProperty, value); }
        }

        protected override void OnAttachedTo(Editor bindable)
        {
            _element = bindable;
            bindable.TextChanged += Bindable_TextChanged;
            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            _element = null;
            BindingContext = null;
            bindable.TextChanged -= Bindable_TextChanged;
            bindable.BindingContextChanged -= OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, System.EventArgs e)
        {
            BindingContext = _element?.BindingContext;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidateCommand != null && ValidateCommand.CanExecute(null))
            {
                ValidateCommand.Execute(null);
            }
        }
    }
}
