<template>
  <div class="component container">
    <div class="row mt-4">
      <div class="col-12 d-flex">
        <img :src="profile.picture" />
        <div class="ms-3 d-flex flex-column justify-content-center">
          <h1>{{ profile.name }}</h1>
          <h5>Reviews: {{ profileReviews.length }}</h5>
          <h5>Average Review: {{ averageRating }}</h5>
        </div>
      </div>
    </div>
    <!-- NO D_FLEX (INCLUDING ROWS) WHEN DOING MASONRY -->
    <div class="masonry-with-columns">
      <div v-for="rev in profileReviews" :key="rev.id">
        <div class="bg-grey p-3 m-2">
          <p>
            {{ rev.restaurantName }}: <i class="mdi mdi-star text-warning"></i
            >{{ rev.rating }}
          </p>
          <p>
            <b>
              <em>"{{ rev.body }}" </em>
            </b>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { onMounted, computed } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger.js'
import { profilesService } from '../services/ProfilesService.js'
import Pop from '../utils/Pop.js'
import { AppState } from '../AppState.js'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id)
        await profilesService.getReviews(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.profile),
      profileReviews: computed(() => AppState.profileReviews),
      averageRating: computed(() => {
        let reviews = AppState.profileReviews
        let total = 0
        reviews.forEach(r => total += r.rating)
        return (total / reviews.length).toFixed(1)
      })
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>