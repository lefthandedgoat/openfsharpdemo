module generated_views

open Suave.Html
open helper_html
open helper_bootstrap
open generated_html
open generated_forms
open generated_data_access
open generated_types
open generator
open helper_general

let brand = "Best Online Store"

let view_jumbo_home =
  base_html
    "Home"
    (base_header brand)
    [
      divClass "container" [
        divClass "jumbotron" [
          h1 (sprintf "Welcome to Best Online Store!")
        ]
      ]
    ]
    scripts.common

let view_thanks =
  base_html
    "Thanks"
    (base_header brand)
    [
      divClass "container" [
        divClass "jumbotron" [
          h1 (sprintf "Thanks!")
        ]
      ]
    ]
    scripts.common

let view_register =
  base_middle_html
    "Register"
    (base_header brand)
    [
      common_register_form
        "Register"
        [
          hiddenInput "UserID" "-1"
          icon_label_text "First Name" "" "user"
          icon_label_text "Last Name" "" "user"
          icon_label_text "Email" "" "envelope"
          icon_password_text "Password" "" "lock"
          icon_password_text "Confirm Password" "" "lock"
        ]
    ]
    scripts.common

let view_errored_register errors (registerForm : RegisterForm) =
  base_middle_html
    "Register"
    (base_header brand)
    [
      common_register_form
        "Register"
        [
          hiddenInput "UserID" registerForm.UserID
          errored_icon_label_text "First Name" registerForm.FirstName "user" errors
          errored_icon_label_text "Last Name" registerForm.LastName "user" errors
          errored_icon_label_text "Email" registerForm.Email "envelope" errors
          errored_icon_password_text "Password" registerForm.Password "lock" errors
          errored_icon_password_text "Confirm Password" registerForm.ConfirmPassword "lock" errors
        ]
    ]
    scripts.common

let view_login error email =
  let errorTag =
    if error
    then stand_alone_error "Invalid email or password"
    else emptyText

  base_middle_html
    "Login"
    (base_header brand)
    [
      common_register_form
        "Login"
        [
          errorTag
          hiddenInput "UserID" "-1"
          icon_label_text "Email" email "envelope"
          icon_password_text "Password" "" "lock"
        ]
    ]
    scripts.common

let view_errored_login errors (loginForm : LoginForm) =
  base_middle_html
    "Login"
    (base_header brand)
    [
      common_register_form
        "Login"
        [
          hiddenInput "UserID" loginForm.UserID
          errored_icon_label_text "Email" loginForm.Email "envelope" errors
          errored_icon_password_text "Password" loginForm.Password "lock" errors
        ]
    ]
    scripts.common

let view_view_product (product : Product) =
  let button = inputAttr [ "value","Add to Cart"; "type","submit"; "class","btn btn-success pull-right"; ]
  base_html
    "Product"
    (base_header brand)
    [
      common_static_form' "/cart/add" button
        "Product"
        [
          hiddenInput "ProductID" product.ProductID
          label_static "Name" product.Name
          label_static "Description" product.Description
          label_static "Price" product.Price
          label_static "Category" product.Category
        ]
    ]
    scripts.common

let view_search_product field how value products =
  let fields = ["Name", "Name"; "Description","Description"; "Category", "Category"]
  let hows = ["Equals", "Equals"; "Begins With","Begins With"]
  let toTr (product : Product) inner =
    trLink (sprintf "/product/view/%i" product.ProductID) inner

  let toTd (product : Product) =
    [
        td [ text (string product.ProductID) ]
        td [ text (string product.Name) ]
        td [ text (string product.Description) ]
        td [ text (string product.Price) ]
        td [ text (string product.Category) ]
    ]

  base_html
    "Search Product"
    (base_header brand)
    [
      container [
        row [
          form_wrapper [
            form_title [ h3Inner "Search Products" [ ] ]
            form_content [
              divClass "search-bar" [
                form_inline [
                  content [
                    inline_label_select_selected "Field" fields field
                    inline_label_select_selected"How" hows how
                    inline_label_text "Value" value
                    button_submit_right
                  ]
                ]
              ]
              content [
                table_bordered_linked_tr
                  [
                    "Product ID"
                    "Name"
                    "Description"
                    "Price"
                    "Category"
                  ]
                  products toTd toTr
              ]
            ]
          ]
        ]
      ]
    ]
    scripts.datatable_bundle

let view_view_cart (cart : Cart) =
  let button = inputAttr [ "value","Checkout"; "type","submit"; "class","btn btn-success pull-right"; ]
  base_html
    "Cart"
    (base_header brand)
    [
      common_static_form' "/checkout" button
        "Cart"
        [
          hiddenInput "CartFK" cart.CartID
          label_static "Number of Items" cart.Items.Length
        ]
    ]
    scripts.common

let view_create_checkout =
  base_html
    "Create Checkout"
    (base_header brand)
    [
      common_form
        "Create Checkout"
        [
          hiddenInput "CheckoutID" "-1"
          label_text "Cart FK" ""
        ]
    ]
    scripts.common
