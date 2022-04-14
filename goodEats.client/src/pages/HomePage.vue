<template>
  <div class="home container">
    <div class="row justify-content-center">
      <div class="col-8 p-3" v-for="r in restaurants" :key="r.id">
        <Restaurant :restaurant="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { restaurantsService } from '../services/RestaurantsService.js'
import { AppState } from '../AppState.js'

export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await restaurantsService.getAll()
      } catch (error) {
        logger.log(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      restaurants: computed(() => AppState.restaurants)
    }

  }
}
</script>

<style scoped lang="scss">
</style>
