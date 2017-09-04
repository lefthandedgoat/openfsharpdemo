module generated_paths

open Suave
open Suave.Filters
open Suave.Successful
open Suave.Operators
open Suave.Model.Binding
open Suave.Form
open Suave.ServerErrors
open forms
open helper_handler
open generated_handlers

type Int64Path = PrintfFormat<(int64 -> string),unit,string,string,int64>

let path_home = "/"
let path_register = "/register"
let path_login = "/login"
let path_create_product = "/product/create"
let path_generate_product : Int64Path = "/product/generate/%i"
let path_view_product : Int64Path = "/product/view/%i"
let path_edit_product : Int64Path = "/product/edit/%i"
let path_list_product = "/product/list"
let path_search_product = "/product/search"
let path_create_cart = "/cart/create"
let path_generate_cart : Int64Path = "/cart/generate/%i"
let path_view_cart : Int64Path = "/cart/view/%i"
let path_edit_cart : Int64Path = "/cart/edit/%i"
let path_list_cart = "/cart/list"
let path_create_cartItem = "/cartItem/create"
let path_generate_cartItem : Int64Path = "/cartItem/generate/%i"
let path_view_cartItem : Int64Path = "/cartItem/view/%i"
let path_edit_cartItem : Int64Path = "/cartItem/edit/%i"
let path_list_cartItem = "/cartItem/list"
let path_create_checkout = "/checkout/create"
let path_generate_checkout : Int64Path = "/checkout/generate/%i"
let path_view_checkout : Int64Path = "/checkout/view/%i"
let path_edit_checkout : Int64Path = "/checkout/edit/%i"
let path_list_checkout = "/checkout/list"

let path_api_register = "/api/register"
let path_api_user : Int64Path = "/api/user/%i"
let path_api_product : Int64Path = "/api/product/%i"
let path_api_create_product = "/api/product/create"
let path_api_edit_product = "/api/product/edit"
let path_api_delete_product : Int64Path = "/api/product/delete/%i"
let path_api_cart : Int64Path = "/api/cart/%i"
let path_api_checkout : Int64Path = "/api/checkout/%i"

let generated_routes =
  [
    path path_home >=> home
    path path_register >=> register
    path path_api_register >=> api_register
    path path_login >=> login
    path path_create_product >=> create_product
    pathScan path_generate_product generate_product
    pathScan path_view_product view_product
    pathScan path_edit_product edit_product
    path path_list_product >=> list_product
    path path_search_product >=> search_product
    path path_create_cart >=> create_cart
    pathScan path_generate_cart generate_cart
    pathScan path_view_cart view_cart
    pathScan path_edit_cart edit_cart
    path path_list_cart >=> list_cart
    path path_create_cartItem >=> create_cartItem
    pathScan path_generate_cartItem generate_cartItem
    pathScan path_view_cartItem view_cartItem
    pathScan path_edit_cartItem edit_cartItem
    path path_list_cartItem >=> list_cartItem
    path path_create_checkout >=> loggedOn path_login create_checkout
    pathScan path_generate_checkout (fun id -> loggedOn path_login (generate_checkout id))
    pathScan path_view_checkout (fun id -> loggedOn path_login (view_checkout id))
    pathScan path_edit_checkout (fun id -> loggedOn path_login (edit_checkout id))
    path path_list_checkout >=> loggedOn path_login list_checkout
    pathScan path_api_product api_product
    path path_api_create_product >=> api_create_product
    path path_api_edit_product >=> api_edit_product
    pathScan path_api_delete_product api_delete_product
    pathScan path_api_cart api_cart
    pathScan path_api_checkout api_checkout
  ]
