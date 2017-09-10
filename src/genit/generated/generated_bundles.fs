module generated_bundles

open forms
open helper_general
open generated_fake_data
open generated_types
open generated_views
open generated_data_access
open generated_forms
open generated_validation

let bundle_product : Bundle<Product, ProductForm> =
    {
      validateForm = Some validation_productForm
      convertForm = Some convert_productForm
      fake_single = Some fake_product
      fake_many = Some fake_many_product
      tryById = Some tryById_product
      getMany = Some getMany_product
      getManyWhere = Some getManyWhere_product
      insert = Some insert_product
      update = Some update_product
      view_list = Some view_list_product
      view_edit = Some view_edit_product
      view_create = Some view_create_product
      view_generate = Some view_generate_product
      view_view = Some view_view_product
      view_search = Some view_search_product
      view_edit_errored = Some view_edit_errored_product
      view_create_errored = Some view_create_errored_product
      view_generate_errored = Some view_generate_errored_product
      href_create = "/product/create"
      href_generate = "/product/generate/%i"
      href_list = "/product/list"
      href_search = "/product/search"
      href_view = "/product/view/%i"
      href_edit = "/product/edit/%i"
    }

let bundle_cart : Bundle<Cart, CartForm> =
    {
      validateForm = Some validation_cartForm
      convertForm = Some convert_cartForm
      fake_single = Some fake_cart
      fake_many = Some fake_many_cart
      tryById = Some tryById_cart
      getMany = Some getMany_cart
      getManyWhere = None
      insert = Some insert_cart
      update = Some update_cart
      view_list = Some view_list_cart
      view_edit = None
      view_create = Some view_create_cart
      view_generate = None
      view_view = Some view_view_cart
      view_search = None
      view_edit_errored = None
      view_create_errored = Some view_create_errored_cart
      view_generate_errored = None
      href_create = "/cart/create"
      href_generate = "/cart/generate/%i"
      href_list = "/cart/list"
      href_search = "/cart/search"
      href_view = "/cart/view/%i"
      href_edit = "/cart/edit/%i"
    }

let bundle_cartItem : Bundle<CartItem, CartItemForm> =
    {
      validateForm = Some validation_cartItemForm
      convertForm = Some convert_cartItemForm
      fake_single = Some fake_cartItem
      fake_many = Some fake_many_cartItem
      tryById = Some tryById_cartItem
      getMany = Some getMany_cartItem
      getManyWhere = None
      insert = Some insert_cartItem
      update = Some update_cartItem
      view_list = Some view_list_cartItem
      view_edit = Some view_edit_cartItem
      view_create = Some view_create_cartItem
      view_generate = Some view_generate_cartItem
      view_view = Some view_view_cartItem
      view_search = None
      view_edit_errored = Some view_edit_errored_cartItem
      view_create_errored = Some view_create_errored_cartItem
      view_generate_errored = Some view_generate_errored_cartItem
      href_create = "/cartItem/create"
      href_generate = "/cartItem/generate/%i"
      href_list = "/cartItem/list"
      href_search = "/cartItem/search"
      href_view = "/cartItem/view/%i"
      href_edit = "/cartItem/edit/%i"
    }

let bundle_checkout : Bundle<Checkout, CheckoutForm> =
    {
      validateForm = Some validation_checkoutForm
      convertForm = None
      fake_single = Some fake_checkout
      fake_many = Some fake_many_checkout
      tryById = Some tryById_checkout
      getMany = Some getMany_checkout
      getManyWhere = None
      insert = Some insert_checkout
      update = Some update_checkout
      view_list = Some view_list_checkout
      view_edit = None
      view_create = Some view_create_checkout
      view_generate = Some view_generate_checkout
      view_view = Some view_view_checkout
      view_search = None
      view_edit_errored = None
      view_create_errored = None
      view_generate_errored = None
      href_create = "/checkout/create"
      href_generate = "/checkout/generate/%i"
      href_list = "/checkout/list"
      href_search = "/checkout/search"
      href_view = "/checkout/view/%i"
      href_edit = "/checkout/edit/%i"
    }
