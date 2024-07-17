namespace Hyperar.HPA.WinUI.Models
{
    using System;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Media;

    internal class SeparatorItemTemplate : MenuItemTemplate
    {
        private const string MissingIconKey = "MissingIcon";

        public SeparatorItemTemplate()
        {
            this.IsSelectable = false;

            object? resource = Application.Current?.FindResource(MissingIconKey);

            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            this.Icon = resource is StreamGeometry icon ? icon : throw new InvalidCastException(nameof(resource));
        }

        public StreamGeometry Icon { get; }
    }
}