//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TodoLocalized.Pages {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("C:\\Users\\mohaned\\Desktop\\SharedProject\\TodoShared\\Pages\\invoice_tab.xaml")]
    public partial class invoice_tab : global::Xamarin.Forms.TabbedPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::TodoLocalized.Pages.invoices_pending Pending;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::TodoLocalized.Pages.Invoice All;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(invoice_tab));
            Pending = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::TodoLocalized.Pages.invoices_pending>(this, "Pending");
            All = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::TodoLocalized.Pages.Invoice>(this, "All");
        }
    }
}
