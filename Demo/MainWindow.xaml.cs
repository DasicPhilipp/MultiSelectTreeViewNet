using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Demo.ViewModel;

namespace Demo
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
            this.InitializeComponent();
            this.ShowSecondCheck_Checked(null, null);

			// Create some example nodes to play with
			TreeItemViewModel rootNode = new TreeItemViewModel(null, false) { DisplayName = "rootNode" };
			TreeItemViewModel node1 = new TreeItemViewModel(rootNode, false) { DisplayName = "element1 (editable)", IsEditable = true };
			TreeItemViewModel node2 = new TreeItemViewModel(rootNode, false) { DisplayName = "element2" };
			TreeItemViewModel node11 = new TreeItemViewModel(node1, false) { DisplayName = "element11", Remarks = "Look at me!" };
			TreeItemViewModel node12 = new TreeItemViewModel(node1, false) { DisplayName = "element12 (disabled)", IsEnabled = false };
			TreeItemViewModel node13 = new TreeItemViewModel(node1, false) { DisplayName = "element13" };
			TreeItemViewModel node131 = new TreeItemViewModel(node13, false) { DisplayName = "element131" };
			TreeItemViewModel node132 = new TreeItemViewModel(node13, false) { DisplayName = "element132" };
			TreeItemViewModel node14 = new TreeItemViewModel(node1, false) { DisplayName = "element14 with colours" };
			ColorItemViewModel colorNode1 = new ColorItemViewModel(node14, false) { Color = Colors.Aqua, IsEditable = true };
			ColorItemViewModel colorNode2 = new ColorItemViewModel(node14, false) { Color = Colors.ForestGreen };
			ColorItemViewModel colorNode3 = new ColorItemViewModel(node14, false) { Color = Colors.LightSalmon };
			TreeItemViewModel node15 = new TreeItemViewModel(node1, true) { DisplayName = "element15 (lazy loading)" };

			// Add them all to each other
			rootNode.Children.Add(node1);
			rootNode.Children.Add(node2);
			node1.Children.Add(node11);
			node1.Children.Add(node12);
			node1.Children.Add(node13);
			node13.Children.Add(node131);
			node13.Children.Add(node132);
			node1.Children.Add(node14);
			node14.Children.Add(colorNode1);
			node14.Children.Add(colorNode2);
			node14.Children.Add(colorNode3);
			node1.Children.Add(node15);

			// Use the root node as the window's DataContext to allow data binding. The TreeView
			// will use the Children property of the DataContext as list of root tree items. This
			// property happens to be the same as each item DataTemplate uses to find its subitems.
            this.DataContext = rootNode;

			// Preset some node states
			node1.IsSelected = true;
			node13.IsSelected = true;
			node14.IsExpanded = true;
		}

		private void TheTreeView_PreviewSelectionChanged(object sender, PreviewSelectionChangedEventArgs e)
		{
			if (this.LockSelectionCheck.IsChecked == true)
			{
				// The current selection is locked by user request (Lock CheckBox is checked).
				// Don't allow any changes to the selection at all.
				e.CancelThis = true;
			}
			else
			{
				// Selection is not locked, apply other conditions.
				// Require all selected items to be of the same type. If an item of another data
				// type is already selected, don't include this new item in the selection.
				if (e.Selecting && this.TheTreeView.SelectedItems.Count > 0)
				{
					e.CancelThis = e.Item.GetType() != this.TheTreeView.SelectedItems[0].GetType();
				}
			}

			//if (e.Selecting)
			//{
			//    System.Diagnostics.Debug.WriteLine("Preview: Selecting " + e.Item + (e.Cancel ? " - cancelled" : ""));
			//}
			//else
			//{
			//    System.Diagnostics.Debug.WriteLine("Preview: Deselecting " + e.Item + (e.Cancel ? " - cancelled" : ""));
			//}
		}

		private void ClearChildrenButton_Click(object sender, RoutedEventArgs e)
		{
			object[] selection = new object[this.TheTreeView.SelectedItems.Count];
            this.TheTreeView.SelectedItems.CopyTo(selection, 0);
			foreach (TreeItemViewModel node in selection)
			{
				if (node.Children != null)
				{
					node.Children.Clear();
				}
			}
		}

		private void AddChildButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.SelectedItems)
			{
				if (!node.HasDummyChild)
				{
					node.Children.Add(new TreeItemViewModel(node, false) { DisplayName = "newborn child" });
					node.IsExpanded = true;
				}
			}
		}

		private void ExpandNodesButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.SelectedItems)
			{
				node.IsExpanded = true;
			}
		}

		private void HideNodesButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.SelectedItems.OfType<TreeItemViewModel>().ToArray())
			{
				node.IsVisible = false;
			}
		}

		private void ShowNodesButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.Items)
			{
                this.DoShowAll(node, (n) => true);
			}
		}

		private void DoShowAll(TreeItemViewModel node, Func<TreeItemViewModel, bool> selector)
		{
			node.IsVisible = selector(node);
			if (node.Children != null)
			{
				foreach (TreeItemViewModel child in node.Children)
				{
                    this.DoShowAll(child, selector);
				}
			}
		}

		private void SelectNoneButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.Items)
			{
                this.DoSelectAll(node, (n) => false);
			}
		}

		private void SelectSomeButton_Click(object sender, RoutedEventArgs e)
		{
			Random rnd = new Random();
			foreach (TreeItemViewModel node in this.TheTreeView.Items)
			{
                this.DoSelectAll(node, (n) => rnd.Next(0, 2) > 0);
			}
		}

		private void SelectAllButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.Items)
			{
                this.DoSelectAll(node, (n) => true);
			}
		}

		private void ToggleSelectButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.Items)
			{
                this.DoSelectAll(node, (n) => !n.IsSelected);
			}
		}

		private void DoSelectAll(TreeItemViewModel node, Func<TreeItemViewModel, bool> selector)
		{
			node.IsSelected = selector(node);
			if (node.Children != null)
			{
				foreach (TreeItemViewModel child in node.Children)
				{
                    this.DoSelectAll(child, selector);
				}
			}
		}

		private void ExpandMenuItem_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.SelectedItems)
			{
				node.IsExpanded = true;
			}
		}

		private void RenameMenuItem_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.SelectedItems)
			{
				node.IsEditing = true;
				break;
			}
		}

		private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
		{
			foreach (TreeItemViewModel node in this.TheTreeView.SelectedItems.Cast<TreeItemViewModel>().ToArray())
			{
				node.Parent.Children.Remove(node);
			}
		}

		private void ShowSecondCheck_Checked(object sender, RoutedEventArgs e)
		{
			if (this.ShowSecondCheck.IsChecked == true)
			{
				if (this.LastColumn.ActualWidth == 0)
                    this.Width += this.FirstColumn.ActualWidth;
                this.LastColumn.Width = new GridLength(1, GridUnitType.Star);
			}
			else
			{
				if (this.LastColumn.ActualWidth > 0)
                    this.Width -= this.LastColumn.ActualWidth;
                this.LastColumn.Width = new GridLength(0, GridUnitType.Pixel);
			}
		}
	}
}
