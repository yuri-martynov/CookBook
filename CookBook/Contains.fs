module Contains

open Functions
open Types

let get (getDishById: getDishById) dishId (product: string) = async {
    let! dish = getDishById dishId
    let contains = 
        dish.ingredients
        |> Seq.exists (fun p -> p.product.name = product )

    if contains then
        return "Да"
    else
        return "Нет"
}

