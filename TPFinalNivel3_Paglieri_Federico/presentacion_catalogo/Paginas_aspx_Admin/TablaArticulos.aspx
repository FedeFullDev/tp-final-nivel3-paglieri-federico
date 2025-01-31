<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="TablaArticulos.aspx.cs" Inherits="presentacion_catalogo.Paginas_aspx_Admin.TablaArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="../Contenido_CSS/TablaArticulos.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <section class="tabla_articulos">

        <div class="articulos_grid_container">
            <asp:GridView CssClass="articulos_grid" DataKeyNames="Id" ID="GridViewArticulos" AutoGenerateColumns="false" runat="server" OnSelectedIndexChanged="GridViewArticulos_SelectedIndexChanged">

    <Columns>
        <asp:BoundField ItemStyle-CssClass="grid_elemento" HeaderText="Artículo" DataField="Nombre" />

        <asp:BoundField ItemStyle-CssClass="grid_elemento" HeaderText="Cod.Artículo" DataField="CodigoArticulo" />
        <asp:BoundField ItemStyle-CssClass="grid_elemento elemento_marca" HeaderText="Marca" DataField="Marca" />
        <asp:BoundField ItemStyle-CssClass="grid_elemento elemento_categoria" HeaderText="Categoría" DataField="Categoria" />
        <asp:BoundField ItemStyle-CssClass="grid_elemento elemento_precio" HeaderText="Precio" DataField="Precio" />
        <%--<asp:CommandField ItemStyle-CssClass="grid_elemento grid_elementoCommand" ShowSelectButton="true" HeaderText="Artículos" SelectText="✍️" />--%>
         <asp:TemplateField ItemStyle-CssClass="grid_elemento grid_elementoCommand" HeaderText="Editar Artículo">
     <ItemTemplate>
         <asp:LinkButton ID="btnSelect" runat="server" CommandName="Select" CssClass="grid_elemento grid_elementoCommand">
         <i class="fa fa-pencil" aria-hidden="true"></i>
        
         </asp:LinkButton>
     </ItemTemplate>
 </asp:TemplateField>
    </Columns>

</asp:GridView>

        </div>
        


        <asp:Button ID="btonAgregarArticulo" CssClass="articulos_agregar" OnClick="btonAgregarArticulo_Click" Text="Agregar Artículo" runat="server" />


    </section>

    <%if (Session["validarTipoUsuarioAdmin"] != null)%>
    <%{%>
    <%if (!(bool)Session["validarTipoUsuarioAdmin"]) %>

    <% {

    %>

    <script>
        let buttonAgregarArticulo = document.getElementById("ContentPlaceHolder1_btonAgregarArticulo");
        buttonAgregarArticulo.style.visibility = "hidden";
    </script>
    <%} %>
    <% else %>
    <%  { %>
    <script>
        let buttonAgregarArticulo = document.getElementById("ContentPlaceHolder1_btonAgregarArticulo");
        buttonAgregarArticulo.style.visibility = "visible";

    </script>
    <%  } %>
    <%} %>


    <script src="../Contenido_JS/PerfilUsuario.js"></script>
</asp:Content>
