import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class ReviewsService {
  async getReviews(id) {
    const res = await api.get(`api/restaurants/${id}/reviews`)
    AppState.reviews = res.data
  }
  async create(body) {
    body.restaurantId = AppState.activeRestaurant.id
    const res = await api.post('api/reviews', body)
    AppState.reviews.push(res.data)
  }
}

export const reviewsService = new ReviewsService()