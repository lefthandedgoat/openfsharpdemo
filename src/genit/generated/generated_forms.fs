module generated_forms

open Suave.Model.Binding
open Suave.Form
open generated_types

type RegisterForm =
  {
    UserID : string
    FirstName : string
    LastName : string
    Email : string
    Password : string
    ConfirmPassword : string
  }

let registerForm : Form<RegisterForm> = Form ([],[])

let convert_registerForm (registerForm : RegisterForm) : Register =
  {
    UserID = int64 registerForm.UserID
    FirstName = registerForm.FirstName
    LastName = registerForm.LastName
    Email = registerForm.Email
    Password = registerForm.Password
  }
  
type LoginForm =
  {
    UserID : string
    Email : string
    Password : string
  }

let loginForm : Form<LoginForm> = Form ([],[])

let convert_loginForm (loginForm : LoginForm) : Login =
  {
    UserID = int64 loginForm.UserID
    Email = loginForm.Email
    Password = loginForm.Password
  }
  
type ProductForm =
  {
    ProductID : string
    Name : string
    Description : string
    Price : string
    Category : string
  }

let productForm : Form<ProductForm> = Form ([],[])

let convert_productForm (productForm : ProductForm) : Product =
  {
    ProductID = int64 productForm.ProductID
    Name = productForm.Name
    Description = productForm.Description
    Price = double productForm.Price
    Category = productForm.Category
  }
  
type CartForm =
  {
    CartID : string
    RegisterFK : string
  }

let cartForm : Form<CartForm> = Form ([],[])

let convert_cartForm (cartForm : CartForm) : Cart =
  {
    CartID = int64 cartForm.CartID
    RegisterFK = int64 cartForm.RegisterFK
  }
  
type CartItemForm =
  {
    CartItemID : string
    CartFK : string
    ProductFK : string
  }

let cartItemForm : Form<CartItemForm> = Form ([],[])

let convert_cartItemForm (cartItemForm : CartItemForm) : CartItem =
  {
    CartItemID = int64 cartItemForm.CartItemID
    CartFK = int64 cartItemForm.CartFK
    ProductFK = int64 cartItemForm.ProductFK
  }
  
type CheckoutForm =
  {
    CheckoutID : string
    CartFK : string
  }

let checkoutForm : Form<CheckoutForm> = Form ([],[])

let convert_checkoutForm (checkoutForm : CheckoutForm) : Checkout =
  {
    CheckoutID = int64 checkoutForm.CheckoutID
    CartFK = int64 checkoutForm.CartFK
  }
  