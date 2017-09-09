module generated_validation

open generated_forms
open generated_types
open validators

let validation_registerForm (registerForm : RegisterForm) =
  [
    validate_required "First Name" registerForm.FirstName
    validate_required "Last Name" registerForm.LastName
    validate_max_length "First Name" 64 registerForm.FirstName
    validate_max_length "Last Name" 64 registerForm.FirstName
    validate_email "Email" registerForm.Email
    validate_required "Email" registerForm.Email
    validate_password "Password" registerForm.Password
    validate_required "Password" registerForm.Password
    validate_password "Confirm Password" registerForm.ConfirmPassword
    validate_equal "Password" "Confirm Password" registerForm.Password registerForm.ConfirmPassword
    validate_required "Confirm Password" registerForm.ConfirmPassword
  ] |> List.choose id

let validation_registerJson (register : Register) =
  [
    validate_required "First Name" register.FirstName
    validate_required "Last Name" register.LastName
    validate_email "Email" register.Email
    validate_required "Email" register.Email
    validate_password "Password" register.Password
    validate_required "Password" register.Password
  ] |> List.choose id

let validation_loginForm (loginForm : LoginForm) =
  [
    validate_email "Email" loginForm.Email
    validate_required "Email" loginForm.Email
    validate_password "Password" loginForm.Password
    validate_required "Password" loginForm.Password
  ] |> List.choose id

let validation_productForm (productForm : ProductForm) =
  [
    validate_required "Name" productForm.Name
    validate_required "Description" productForm.Description
    validate_double "Price" productForm.Price
    validate_required "Price" productForm.Price
    validate_required "Category" productForm.Category
  ] |> List.choose id

let validation_productJson (product : Product) =
  [
    validate_required "Name" product.Name
    validate_required "Description" product.Description
    validate_double "Price" product.Price
    validate_required "Price" product.Price
    validate_required "Category" product.Category
  ] |> List.choose id

let validation_cartForm (cartForm : CartForm) =
  [
    validate_integer "Register FK" cartForm.RegisterFK
  ] |> List.choose id

let validation_cartJson (cart: Cart) =
  [
    validate_required "User FK" cart.UserFK
  ] |> List.choose id

let validation_cartItemForm (cartItemForm : CartItemForm) =
  [
    validate_integer "Cart FK" cartItemForm.CartFK
    validate_integer "Product FK" cartItemForm.ProductFK
  ] |> List.choose id

let validation_cartItemJson (cartItem: CartItem) =
  [
    validate_required "Cart FK" cartItem.CartFK
    validate_required "Product FK" cartItem.ProductFK
  ] |> List.choose id

let validation_checkoutForm (checkoutForm : CheckoutForm) =
  [
    validate_integer "Cart FK" checkoutForm.CartFK
  ] |> List.choose id

let validation_checkoutJson (checkout : Checkout) =
  [
    validate_required "Cart FK" checkout.CartFK
  ] |> List.choose id
