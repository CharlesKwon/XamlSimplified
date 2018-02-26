using Sample._028_DataTemplate;
using System.Windows;
using System.Windows.Controls;

namespace Sample.Selector
{
    public class MessageStyleSelector : StyleSelector
    {
        public Style LeftStyle { get; set; }
        public Style RightStyle { get; set; }
        public string PropertyToCheck { get; set; }
        public string PropertyValue { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var exchangeMessage = (ExchangeMessage)item;
            var type = exchangeMessage.GetType();
            var property = type.GetProperty(PropertyToCheck);

            if (property.GetValue(exchangeMessage, null).ToString() == PropertyValue)
            {
                return RightStyle;
            }
            else
            {
                return LeftStyle;
            }
        }
    }

    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LeftTemplate { get; set; }
        public DataTemplate RightTemplate { get; set; }
        public string PropertyToCheck { get; set; }
        public string PropertyValue { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var exchangeMessage = (ExchangeMessage)item;
            var type = exchangeMessage.GetType();
            var property = type.GetProperty(PropertyToCheck);

            if (property.GetValue(exchangeMessage, null).ToString() == PropertyValue)
            {
                return RightTemplate;
            }
            else
            {
                return LeftTemplate;
            }
        }
    }
}
