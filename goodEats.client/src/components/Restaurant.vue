<template>
  <div
    class="bg-white rounded elevation-2 d-flex selectable select-grow"
    data-bs-toggle="modal"
    data-bs-target="#active-restaurant"
    @click="setActive"
  >
    <img
      class="object-fit-cover"
      width="200"
      height="200"
      :src="restaurant.picture"
      :alt="restaurant.name + ' picture'"
    />
    <div class="d-flex flex-column justify-content-center ms-3">
      <h3 class="m-0 border-bottom border-dark p-1 mb-1">
        {{ restaurant.name }} | {{ restaurant.category }}
        <i class="mdi mdi-star text-warning"></i
        >{{ restaurant.averageRating.toFixed(1) }}
      </h3>
      <p class="m-0">{{ restaurant.address }}</p>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { reviewsService } from '../services/ReviewsService.js'
export default {
  props: {
    restaurant: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      async setActive() {
        try {
          AppState.activeRestaurant = props.restaurant
          await reviewsService.getReviews(props.restaurant.id)
        } catch (error) {
          // close modal?
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>