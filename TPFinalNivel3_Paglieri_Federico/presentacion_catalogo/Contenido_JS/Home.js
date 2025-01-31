document.addEventListener("DOMContentLoaded", () => {
    const contenedorProductos = document.querySelectorAll(".catalogo_producto");

    contenedorProductos.forEach((producto) => {
        const iconContainer = producto.querySelector("#spanIconProductoFavorito");
        const iconStar = iconContainer?.querySelector(".fa-star");
        const iconTrash = iconContainer?.querySelector(".fa-trash");
        const textProductoFavoritoDescription = iconContainer?.querySelector("#iconProductoFavoritoDescripcionFuncionalidad");
        const textProductoEliminadoDescription = iconContainer?.querySelector("#iconProductoEliminadoDescripcionFuncionalidad");

        if (!iconContainer || !textProductoFavoritoDescription || !textProductoEliminadoDescription) {
            console.log("Faltan elementos en el DOM:");
            if (!iconContainer) console.log("- iconContainer is missing");
            if (!textProductoFavoritoDescription) console.log("- textProductoFavoritoDescription is missing");
            if (!textProductoEliminadoDescription) console.log("- textProductoEliminadoDescription is missing");
            return;
        }

        console.log("Elementos encontrados:", {
            iconContainer,
            iconStar,
            iconTrash,
            textProductoFavoritoDescription,
            textProductoEliminadoDescription
        });

        // Funcionalidad de iconos al entrar/salir del contenedor del producto
        producto.addEventListener("mouseenter", () => {
            if (iconStar) {
                iconStar.style.visibility = "visible";
                console.log("iconStar visible");
            }
            if (iconTrash) {
                iconTrash.style.visibility = "visible";
                console.log("iconTrash visible");
            }
        });

        producto.addEventListener("mouseleave", () => {
            if (iconStar) {
                iconStar.style.visibility = "hidden";
                console.log("iconStar hidden");
            }
            if (iconTrash) {
                iconTrash.style.visibility = "hidden";
                console.log("iconTrash hidden");
            }
            textProductoFavoritoDescription.style.display = "none";
            textProductoEliminadoDescription.style.display = "none";
        });

        // Funcionalidad de icono de favorito
        if (iconStar) {
            iconStar.addEventListener("mouseenter", () => {
                textProductoFavoritoDescription.style.display = "inline";
                console.log("textProductoFavoritoDescription inline");
            });

            iconStar.addEventListener("mouseleave", () => {
                textProductoFavoritoDescription.style.display = "none";
                console.log("textProductoFavoritoDescription none");
            });
        }

        // Funcionalidad de icono de eliminar
        if (iconTrash) {
            iconTrash.addEventListener("mouseenter", () => {
                textProductoEliminadoDescription.style.display = "inline";
                console.log("textProductoEliminadoDescription inline");
            });

            iconTrash.addEventListener("mouseleave", () => {
                textProductoEliminadoDescription.style.display = "none";
                console.log("textProductoEliminadoDescription none");
            });
        }
    });
});