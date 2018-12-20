using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IOC_PBIAdmin;

namespace PBIAdminApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TokenService.FetchToken();
            Refresh();
        }

        private void Refresh()
        {
            if(TokenService.HasAuthenticatedUser)
            {
                expUser.Header = TokenService.AuthenticatedUser;
                lblName.Content = TokenService.AuthenticatedUserEmail;

                twWorkspaceHierarchy.Items.Clear();
                PBIGroups groups = PBIManagerREST.GetGroups();

                foreach (PBIGroup group in groups.List)
                {
                    //http://blogs.microsoft.co.il/pavely/2014/07/12/data-binding-for-a-wpf-treeview/
                    TreeViewItem g = new TreeViewItem() { Header = group.name, Tag = "Group", Uid = group.id };
                    //Add image to Group level
                    PBIReports reports = PBIManagerREST.GetReports(group.id);

                    foreach (PBIReport report in reports.List)
                    {
                        TreeViewItem r = new TreeViewItem() { Header = report.name, Tag = "Report", Uid = report.id };
                        g.Items.Add(r);
                    }

                    twWorkspaceHierarchy.Items.Add(g);
                }
            }
            else
            { 
                expUser.Header= "Sign in";
                //hide expander icon
            }
        }

        private void WorkspaceHierarchy_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem selectedItem = (TreeViewItem)twWorkspaceHierarchy.SelectedItem;
            if (selectedItem.Tag.ToString() == "Report")
            {
                PBIRefreshes refreshes = PBIManagerREST.GetRefreshes(((TreeViewItem)selectedItem.Parent).Uid, selectedItem.Uid);
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TokenService.ClearCache();
            Refresh();
        }
    }
}
