using System.Windows;
using System.Windows.Media;

namespace SolutionColors
{
    public static class WpfExtensions
    {
        public static T FindChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName)
                {
                    return frameworkElement as T;
                }
            }

            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                child = FindChild<T>(child, childName);

                if (child != null)
                {
                    return child as T;
                }
            }

            return null;
        }

        public static FrameworkElement FindChildByItsType(
            this DependencyObject root,
            string type
            )
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            int childrenCount = VisualTreeHelper.GetChildrenCount(root);

            for (int cc = 0; cc < childrenCount; cc++)
            {
                DependencyObject control = VisualTreeHelper.GetChild(
                    root,
                    cc
                    );

                if (control is FrameworkElement fe)
                {
                    if (fe.GetType().Name == type)
                    {
                        return fe;
                    }
                }

                FrameworkElement result = control.FindChildByItsType(type);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

    }
}
