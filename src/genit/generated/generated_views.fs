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

let view_create_product =
  base_html
    "Create Product"
    (base_header brand)
    [
      common_form
        "Create Product"
        [
          hiddenInput "ProductID" "-1"
          label_text "Name" ""
          label_text "Description" ""
          label_text "Price" ""
          label_text "Category" ""
        ]
    ]
    scripts.common

let view_create_errored_product errors (productForm : ProductForm) =
  base_html
    "Create Product"
    (base_header brand)
    [
      common_form
        "Create Product"
        [
          hiddenInput "ProductID" productForm.ProductID
          errored_label_text "Name" (string productForm.Name) errors
          errored_label_text "Description" (string productForm.Description) errors
          errored_label_text "Price" (string productForm.Price) errors
          errored_label_text "Category" (string productForm.Category) errors
        ]
    ]
    scripts.common

let view_generate_product (product : Product) =
  base_html
    "Generate Product"
    (base_header brand)
    [
      common_form
        "Generate Product"
        [
          hiddenInput "ProductID" product.ProductID
          label_text "Name" product.Name
          label_text "Description" product.Description
          label_text "Price" product.Price
          label_text "Category" product.Category
        ]
    ]
    scripts.common

let view_generate_errored_product errors (productForm : ProductForm) =
  base_html
    "Generate Product"
    (base_header brand)
    [
      common_form
        "Generate Product"
        [
          hiddenInput "ProductID" productForm.ProductID
          errored_label_text "Name" (string productForm.Name) errors
          errored_label_text "Description" (string productForm.Description) errors
          errored_label_text "Price" (string productForm.Price) errors
          errored_label_text "Category" (string productForm.Category) errors
        ]
    ]
    scripts.common

let view_view_product (product : Product) =
  let button = button_small_success_right (sprintf "/product/edit/%i" product.ProductID) [ text "Add to Cart" ]
  base_html
    "Product"
    (base_header brand)
    [
      common_static_form button
        "Product"
        [

          label_static "Name" product.Name
          label_static "Description" product.Description
          label_static "Price" product.Price
          label_static "Category" product.Category
        ]
    ]
    scripts.common

let view_edit_product (product : Product) =
  base_html
    "Edit Product"
    (base_header brand)
    [
      common_form
        "Edit Product"
        [
          hiddenInput "ProductID" product.ProductID
          label_text "Name" product.Name
          label_text "Description" product.Description
          label_text "Price" product.Price
          label_text "Category" product.Category
        ]
    ]
    scripts.common

let view_edit_errored_product errors (productForm : ProductForm) =
  base_html
    "Edit Product"
    (base_header brand)
    [
      common_form
        "Edit Product"
        [
          hiddenInput "ProductID" productForm.ProductID
          errored_label_text "Name" (string productForm.Name) errors
          errored_label_text "Description" (string productForm.Description) errors
          errored_label_text "Price" (string productForm.Price) errors
          errored_label_text "Category" (string productForm.Category) errors
        ]
    ]
    scripts.common

let view_list_product products =
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
    "List Product"
    (base_header brand)
    [
      container [
        row [
          form_wrapper [
            form_title [ h3Inner "List Products" [ button_small_success_right "/product/create" [ text "Create"] ] ]
            form_content [
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

let view_create_cart =
  base_html
    "Create Cart"
    (base_header brand)
    [
      common_form
        "Create Cart"
        [
          hiddenInput "CartID" "-1"
          label_text "User FK" ""
        ]
    ]
    scripts.common

let view_create_errored_cart errors (cartForm : CartForm) =
  base_html
    "Create Cart"
    (base_header brand)
    [
      common_form
        "Create Cart"
        [
          hiddenInput "CartID" cartForm.CartID
          errored_label_text "User FK" (string cartForm.RegisterFK) errors
        ]
    ]
    scripts.common

let view_generate_cart (cart : Cart) =
  base_html
    "Generate Cart"
    (base_header brand)
    [
      common_form
        "Generate Cart"
        [
          hiddenInput "CartID" cart.CartID
          label_text "User FK" cart.UserFK
        ]
    ]
    scripts.common

let view_generate_errored_cart errors (cartForm : CartForm) =
  base_html
    "Generate Cart"
    (base_header brand)
    [
      common_form
        "Generate Cart"
        [
          hiddenInput "CartID" cartForm.CartID
          errored_label_text "User FK" (string cartForm.RegisterFK) errors
        ]
    ]
    scripts.common

let view_view_cart (cart : Cart) =
  let button = button_small_success_right (sprintf "/cart/edit/%i" cart.CartID) [ text "Edit" ]
  base_html
    "Cart"
    (base_header brand)
    [
      common_static_form button
        "Cart"
        [

          label_static "User FK" cart.UserFK
        ]
    ]
    scripts.common

let view_edit_cart (cart : Cart) =
  base_html
    "Edit Cart"
    (base_header brand)
    [
      common_form
        "Edit Cart"
        [
          hiddenInput "CartID" cart.CartID
          label_text "User FK" cart.UserFK
        ]
    ]
    scripts.common

let view_edit_errored_cart errors (cartForm : CartForm) =
  base_html
    "Edit Cart"
    (base_header brand)
    [
      common_form
        "Edit Cart"
        [
          hiddenInput "CartID" cartForm.CartID
          errored_label_text "User FK" (string cartForm.RegisterFK) errors
        ]
    ]
    scripts.common

let view_list_cart carts =
  let toTr (cart : Cart) inner =
    trLink (sprintf "/cart/view/%i" cart.CartID) inner

  let toTd (cart : Cart) =
    [
        td [ text (string cart.CartID) ]
        td [ text (string cart.UserFK) ]
    ]

  base_html
    "List Cart"
    (base_header brand)
    [
      container [
        row [
          form_wrapper [
            form_title [ h3Inner "List Carts" [ button_small_success_right "/cart/create" [ text "Create"] ] ]
            form_content [
              content [
                table_bordered_linked_tr
                  [
                    "Cart ID"
                    "User FK"
                  ]
                  carts toTd toTr
              ]
            ]
          ]
        ]
      ]
    ]
    scripts.datatable_bundle

let view_create_cartItem =
  base_html
    "Create CartItem"
    (base_header brand)
    [
      common_form
        "Create CartItem"
        [
          hiddenInput "CartItemID" "-1"
          label_text "Cart FK" ""
          label_text "Product FK" ""
        ]
    ]
    scripts.common

let view_create_errored_cartItem errors (cartItemForm : CartItemForm) =
  base_html
    "Create CartItem"
    (base_header brand)
    [
      common_form
        "Create CartItem"
        [
          hiddenInput "CartItemID" cartItemForm.CartItemID
          errored_label_text "Cart FK" (string cartItemForm.CartFK) errors
          errored_label_text "Product FK" (string cartItemForm.ProductFK) errors
        ]
    ]
    scripts.common

let view_generate_cartItem (cartItem : CartItem) =
  base_html
    "Generate CartItem"
    (base_header brand)
    [
      common_form
        "Generate CartItem"
        [
          hiddenInput "CartItemID" cartItem.CartItemID
          label_text "Cart FK" cartItem.CartFK
          label_text "Product FK" cartItem.ProductFK
        ]
    ]
    scripts.common

let view_generate_errored_cartItem errors (cartItemForm : CartItemForm) =
  base_html
    "Generate CartItem"
    (base_header brand)
    [
      common_form
        "Generate CartItem"
        [
          hiddenInput "CartItemID" cartItemForm.CartItemID
          errored_label_text "Cart FK" (string cartItemForm.CartFK) errors
          errored_label_text "Product FK" (string cartItemForm.ProductFK) errors
        ]
    ]
    scripts.common

let view_view_cartItem (cartItem : CartItem) =
  let button = button_small_success_right (sprintf "/cartItem/edit/%i" cartItem.CartItemID) [ text "Edit" ]
  base_html
    "CartItem"
    (base_header brand)
    [
      common_static_form button
        "CartItem"
        [

          label_static "Cart FK" cartItem.CartFK
          label_static "Product FK" cartItem.ProductFK
        ]
    ]
    scripts.common

let view_edit_cartItem (cartItem : CartItem) =
  base_html
    "Edit CartItem"
    (base_header brand)
    [
      common_form
        "Edit CartItem"
        [
          hiddenInput "CartItemID" cartItem.CartItemID
          label_text "Cart FK" cartItem.CartFK
          label_text "Product FK" cartItem.ProductFK
        ]
    ]
    scripts.common

let view_edit_errored_cartItem errors (cartItemForm : CartItemForm) =
  base_html
    "Edit CartItem"
    (base_header brand)
    [
      common_form
        "Edit CartItem"
        [
          hiddenInput "CartItemID" cartItemForm.CartItemID
          errored_label_text "Cart FK" (string cartItemForm.CartFK) errors
          errored_label_text "Product FK" (string cartItemForm.ProductFK) errors
        ]
    ]
    scripts.common

let view_list_cartItem cartItems =
  let toTr (cartItem : CartItem) inner =
    trLink (sprintf "/cartItem/view/%i" cartItem.CartItemID) inner

  let toTd (cartItem : CartItem) =
    [
        td [ text (string cartItem.CartItemID) ]
        td [ text (string cartItem.CartFK) ]
        td [ text (string cartItem.ProductFK) ]
    ]

  base_html
    "List CartItem"
    (base_header brand)
    [
      container [
        row [
          form_wrapper [
            form_title [ h3Inner "List CartItems" [ button_small_success_right "/cartItem/create" [ text "Create"] ] ]
            form_content [
              content [
                table_bordered_linked_tr
                  [
                    "CartItem ID"
                    "Cart FK"
                    "Product FK"
                  ]
                  cartItems toTd toTr
              ]
            ]
          ]
        ]
      ]
    ]
    scripts.datatable_bundle

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

let view_create_errored_checkout errors (checkoutForm : CheckoutForm) =
  base_html
    "Create Checkout"
    (base_header brand)
    [
      common_form
        "Create Checkout"
        [
          hiddenInput "CheckoutID" checkoutForm.CheckoutID
          errored_label_text "Cart FK" (string checkoutForm.CartFK) errors
        ]
    ]
    scripts.common

let view_generate_checkout (checkout : Checkout) =
  base_html
    "Generate Checkout"
    (base_header brand)
    [
      common_form
        "Generate Checkout"
        [
          hiddenInput "CheckoutID" checkout.CheckoutID
          label_text "Cart FK" checkout.CartFK
        ]
    ]
    scripts.common

let view_generate_errored_checkout errors (checkoutForm : CheckoutForm) =
  base_html
    "Generate Checkout"
    (base_header brand)
    [
      common_form
        "Generate Checkout"
        [
          hiddenInput "CheckoutID" checkoutForm.CheckoutID
          errored_label_text "Cart FK" (string checkoutForm.CartFK) errors
        ]
    ]
    scripts.common

let view_view_checkout (checkout : Checkout) =
  let button = button_small_success_right (sprintf "/checkout/edit/%i" checkout.CheckoutID) [ text "Edit" ]
  base_html
    "Checkout"
    (base_header brand)
    [
      common_static_form button
        "Checkout"
        [

          label_static "Cart FK" checkout.CartFK
        ]
    ]
    scripts.common

let view_edit_checkout (checkout : Checkout) =
  base_html
    "Edit Checkout"
    (base_header brand)
    [
      common_form
        "Edit Checkout"
        [
          hiddenInput "CheckoutID" checkout.CheckoutID
          label_text "Cart FK" checkout.CartFK
        ]
    ]
    scripts.common

let view_edit_errored_checkout errors (checkoutForm : CheckoutForm) =
  base_html
    "Edit Checkout"
    (base_header brand)
    [
      common_form
        "Edit Checkout"
        [
          hiddenInput "CheckoutID" checkoutForm.CheckoutID
          errored_label_text "Cart FK" (string checkoutForm.CartFK) errors
        ]
    ]
    scripts.common

let view_list_checkout checkouts =
  let toTr (checkout : Checkout) inner =
    trLink (sprintf "/checkout/view/%i" checkout.CheckoutID) inner

  let toTd (checkout : Checkout) =
    [
        td [ text (string checkout.CheckoutID) ]
        td [ text (string checkout.CartFK) ]
    ]

  base_html
    "List Checkout"
    (base_header brand)
    [
      container [
        row [
          form_wrapper [
            form_title [ h3Inner "List Checkouts" [ button_small_success_right "/checkout/create" [ text "Create"] ] ]
            form_content [
              content [
                table_bordered_linked_tr
                  [
                    "Checkout ID"
                    "Cart FK"
                  ]
                  checkouts toTd toTr
              ]
            ]
          ]
        ]
      ]
    ]
    scripts.datatable_bundle
