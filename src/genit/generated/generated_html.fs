module generated_html

open System
open Suave.Html
open helper_html
open helper_bootstrap

let base_header brand =
  navClass "navbar navbar-default" [
    container [
      navbar_header [
        buttonAttr
          ["type","button"; "class","navbar-toggle collapsed"; "data-toggle","collapse"; "data-target","#navbar"; "aria-expanded","false"; "aria-controls","navbar" ]
          [
            spanClass "sr-only" [text "Toggle navigation"]
            spanClass "icon-bar" [emptyText]
            spanClass "icon-bar" [emptyText]
            spanClass "icon-bar" [emptyText]
          ]
        navbar_brand [ text brand ]
      ]
      navbar [
        navbar_nav [
          dropdown [
            dropdown_toggle [text "Pages "; caret]
            dropdown_menu [
              li [ aHref "/" [text "Home"] ]
              li [ aHref "/register" [text "Register"] ]
              li [ aHref "/login" [text "Login"] ]
              li [ aHref "/product/create" [text "Create Product"] ]
              li [ aHref "/product/list" [text "List Products"] ]
              li [ aHref "/product/search" [text "Search Products"] ]
              li [ aHref "/cart/create" [text "Create Cart"] ]
              li [ aHref "/cart/list" [text "List Carts"] ]
              li [ aHref "/cartItem/create" [text "Create CartItem"] ]
              li [ aHref "/cartItem/list" [text "List CartItems"] ]
              li [ aHref "/checkout/create" [text "Create Checkout"] ]
              li [ aHref "/checkout/list" [text "List Checkouts"] ]
            ]
          ]
        ]
      ]
    ]
  ]

  