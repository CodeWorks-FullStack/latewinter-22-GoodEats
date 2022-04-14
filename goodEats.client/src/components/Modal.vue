<template>
  <div class="modal fade" tabindex="-1">
    <div
      class="
        modal-dialog modal-xl modal-dialog-centered modal-fullscreen-md-down
      "
    >
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">
            <slot name="modal-title"></slot>
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body p-md-0">
          <slot name="modal-body"></slot>
        </div>
      </div>
    </div>
  </div>
</template>
