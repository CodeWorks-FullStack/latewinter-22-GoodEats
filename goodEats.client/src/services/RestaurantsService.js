import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class RestaurantsService {
  async getAll() {
    const res = await api.get('api/restaurants')
    AppState.restaurants = res.data
  }

}

export const restaurantsService = new RestaurantsService()