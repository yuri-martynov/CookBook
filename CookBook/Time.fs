module Time

let get dishId : Async<string> = async {
    let! dish = Data.get dishId
    return dish.Time.Value.ToString()
}

