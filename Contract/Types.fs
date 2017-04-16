namespace Types

open System

type Quantity =
    | Items of float
    | Grams of int
    | Liters of float
    | TableSpoons of float
    | ToTaste
    

type Product =
    { name: string
    }

type Ingredient =
    { product: Product
    ; quantity: Quantity
    }

type Step =
    { duration: TimeSpan
    ; description: string
    }

type Recipe =
    { steps: Step seq
    }

type Dish = 
    { name: string
    ; ingredients: Ingredient seq
    ; recipe: Recipe
    }





