<template>
  <v-app>
    <v-app-bar
      app
      color="#F2F2F2"
      dark
      height="100"
    >
    <div class="">
        <v-img
          alt="Logo"
          class="shrink mr-2"
          contain
          src="./assets/mais_a.png"
          transition="scale-transition"
          width="100"
        />
      </div>
    </v-app-bar>
    <v-main>
      <AcademicModule/>      
    </v-main>
  </v-app>
</template>

<script>
import AcademicModule from './components/AcademicModule.vue';

export default {
  name: 'App',

  components: {    
    AcademicModule,
},

  data: () => ({
    //
  }),
};
</script>
