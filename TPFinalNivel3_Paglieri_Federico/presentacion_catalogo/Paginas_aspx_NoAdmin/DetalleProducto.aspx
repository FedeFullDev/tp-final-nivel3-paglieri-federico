<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="presentacion_catalogo.Paginas_aspx_NoAdmin.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Contenido_CSS/DetalleProducto.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="info_articulo">


        <section class="articulo_datos">

            <article class="datos_1">
                <asp:Label runat="server" CssClass="label">Precio<asp:TextBox ID="tBoxPrecio" CssClass="datos"  ReadOnly="true" runat="server"></asp:TextBox></asp:Label>

                <asp:Label runat="server" CssClass="label">Nombre<asp:TextBox ID="tBoxNombre" CssClass="datos datos_MultiLine" TextMode="MultiLine" ReadOnly="true" runat="server"></asp:TextBox></asp:Label>

                

            </article>

            <article class="datos_1">
                <asp:Label runat="server" CssClass="label">Categoría<asp:TextBox ID="tBoxCategoria" CssClass="datos" ReadOnly="true" runat="server"></asp:TextBox></asp:Label>

                <asp:Label runat="server" CssClass="label">Código de artículo<asp:TextBox ID="tBoxCodigoArticulo" CssClass="datos datos_MultiLine" TextMode="MultiLine" ReadOnly="true" runat="server"></asp:TextBox></asp:Label>

            </article>

            <article class="datos_2">

                <asp:Label runat="server" CssClass="label">Marca<asp:TextBox ID="tBoxMarca" CssClass="datos" ReadOnly="true" runat="server"></asp:TextBox></asp:Label>

                <asp:Label runat="server" CssClass="label">Descripción<strong><asp:TextBox ID="tBoxDescripcion" CssClass="datos datos_MultiLine" TextMode="Multiline" ReadOnly="true" runat="server"></asp:TextBox></strong></asp:Label>


            </article>




        </section>

    </section>



</asp:Content>
