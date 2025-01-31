document.addEventListener("DOMContentLoaded", event => {

    const navLinks = document.querySelectorAll(".li_link");
    const navLi = document.querySelectorAll(".lista_li");
    let currentPath = window.location.pathname;

   

    navLinks.forEach((links, indice) => {
        if (currentPath.includes(links.getAttribute("href"))) {
            navLi[indice].classList.remove("lista_li", "li_transform");
            navLi[indice].classList.add("li_pagina_actual");
        }


    })

    
})