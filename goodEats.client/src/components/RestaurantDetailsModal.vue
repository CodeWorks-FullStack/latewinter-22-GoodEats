<template>
  <Modal id="active-restaurant">
    <template #modal-title> {{ restaurant.name }} </template>
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6 p-0">
            <img
              height="500"
              class="w-100 object-fit-cover"
              :src="restaurant.picture"
              alt=""
            />
          </div>
          <div class="col-md-6 p-2">
            <div class="mt-3">
              <h3>
                {{ restaurant.name }} <i class="mdi mdi-star text-warning"></i
                >{{ restaurant.averageRating?.toFixed(1) }}
              </h3>
              <p>{{ restaurant.address }}</p>
            </div>
            <!-- TODO add form for creating review if logged in -->
            <form @submit.prevent="createReview">
              <input type="text" v-model="review.body" />
              <input type="number" max="5" min="1" v-model="review.rating" />
              <button type="submit">Create</button>
            </form>
            <div class="right-col">
              <div v-for="rev in reviews" :key="rev.id">
                <div class="bg-grey p-3 m-2">
                  <p>
                    <b>
                      <em>"{{ rev.body }}"</em>
                    </b>
                  </p>
                  <p
                    class="text-end selectable"
                    @click="goToProfile(rev.creator.id)"
                  >
                    -{{ rev.creator.name }}
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { reviewsService } from '../services/ReviewsService.js'
export default {
  setup() {
    const router = useRouter()
    const review = ref({})
    return {
      review,
      restaurant: computed(() => AppState.activeRestaurant),
      reviews: computed(() => AppState.reviews),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-restaurant')).hide()
        router.push({ name: 'Profile', params: { id } })
      },
      createReview() {
        reviewsService.create(review.value)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.right-col {
  max-height: 400px;
  overflow-y: auto;
}
</style>