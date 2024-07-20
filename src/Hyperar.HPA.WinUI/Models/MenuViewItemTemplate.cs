namespace Hyperar.HPA.WinUI.Models
{
    using System;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Media;
    using WinUI.Enums;

    internal class MenuViewItemTemplate : MenuItemTemplate
    {
        private const string MissingIconKey = "MissingIcon";

        public MenuViewItemTemplate(string text, ViewType viewType) : this(text, viewType, MissingIconKey)
        {
        }

        public MenuViewItemTemplate(string text, ViewType viewType, string iconResourceKey)
        {
            this.IsSelectable = true;
            this.Text = text;
            this.ViewType = viewType;

            object? resource = Application.Current?.FindResource(iconResourceKey) ?? Application.Current?.FindResource(MissingIconKey);

            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            this.Icon = resource is StreamGeometry icon ? icon : throw new InvalidCastException(nameof(resource));
        }

        public StreamGeometry Icon { get; }

        public string Text { get; }

        public ViewType ViewType { get; }
    }
}