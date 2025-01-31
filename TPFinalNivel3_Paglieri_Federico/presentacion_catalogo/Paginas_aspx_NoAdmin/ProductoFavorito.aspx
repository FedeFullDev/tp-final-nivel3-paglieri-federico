<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="ProductoFavorito.aspx.cs" Inherits="presentacion_catalogo.Paginas_aspx_NoAdmin.ProductoFavorito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Contenido_CSS/ProductoFavorito.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%if (Session["listArticulosFavoritos"] != null && ((List<Modelo.Articulo>)Session["listArticulosFavoritos"]).Count > 0)%>
    <%{%>

    <section class="tabla_articulos">

        <div class="articulos_grid_container">
            <asp:GridView CssClass="articulos_grid" DataKeyNames="Id" ID="GridViewArticulosFavoritos" AutoGenerateColumns="false" runat="server" OnSelectedIndexChanged="GridViewArticulosFavoritos_SelectedIndexChanged">

                <Columns>
                    <asp:BoundField ItemStyle-CssClass="grid_elemento" HeaderText="Artículo" DataField="Nombre" />

                    <asp:BoundField ItemStyle-CssClass="grid_elemento" HeaderText="Cod.Artículo" DataField="CodigoArticulo" />
                    <asp:BoundField ItemStyle-CssClass="grid_elemento elemento_marca" HeaderText="Marca" DataField="Marca" />
                    <asp:BoundField ItemStyle-CssClass="grid_elemento elemento_categoria" HeaderText="Categoría" DataField="Categoria" />
                    <asp:BoundField ItemStyle-CssClass="grid_elemento elemento_precio" HeaderText="Precio" DataField="Precio" />
                    <asp:TemplateField ItemStyle-CssClass="grid_elemento grid_elementoCommand" HeaderText="Ubicación del producto en el catálogo">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnSelect" runat="server" CommandName="Select" CssClass="grid_elemento grid_elementoCommand">
                            <i class="fas fa-paperclip"></i>
        
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>

            </asp:GridView>

        </div>






    </section>
    <%}%>
    <%else%>
    <%{%>
    <section class="tabla_articulos">
        <h1 class="articulos_0">No tiene productos favoritos</h1>
    </section>
    <%}%>
</asp:Content>

