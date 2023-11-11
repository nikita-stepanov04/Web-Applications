<template>
  <footer class="bg-light py-2">
    <div class="text-center">
      <h3>Restaurant</h3>
      <p>Address: Kuiv, Shevchenka str. 15</p>
        <div class="contacts">
          <a class="me-2" href="#">
            <font-awesome-icon :icon="['fab', 'telegram']" />
          </a>
          <a class="me-2" href="tel:+380777777777">
            <font-awesome-icon :icon="['fas', 'phone']" />
          </a>
          <a class="me-2" href="mailto:mailcafe@gmail.com">
            <font-awesome-icon :icon="['fas', 'envelope']" />
          </a>
          <a class="me-2" href="#">
            <font-awesome-icon :icon="['fab', 'viber']" />
          </a>
        </div>
    </div>
  </footer>
</template>