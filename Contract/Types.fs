namespace Types

open System

type Quantity =
    | Items of float
    | Grams of int
    | Liters of float
    | Glasses of float
    | TableSpoons of float
    | ToTaste
    

type Product =
    | Whole of string
    | Part of (Product * string)

type ProductQuantity =
    { product: Product
    ; quantity: Quantity
    }

type Ingredient =
    | Only of ProductQuantity
    | Optional of ProductQuantity
    | Xor of Ingredient seq
    | And of Ingredient seq


type Action =
    | Cook of string
    | Background of string

type Step =
    { duration: TimeSpan
    ; action: Action
    }



type Recipe =
    { steps: Step seq
    }

type Dish = 
    { name: string
    ; ingredients: Ingredient seq
    ; recipe: Recipe
    }