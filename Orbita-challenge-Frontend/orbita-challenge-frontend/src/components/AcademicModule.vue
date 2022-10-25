<template>
  <v-layout>
    <v-card 
      height="400"
      width="256"      
      >
      <v-navigation-drawer permanent>
        <v-toolbar fixed dark>
          <v-list>
            <v-list-item>
              <v-list-item-title>
                Módulo Acadêmico
              </v-list-item-title>
            </v-list-item>
          </v-list>
        </v-toolbar>
        <v-divider></v-divider>
        <v-list dense nav>
          <v-list-item v-for="item in items" :key="item.title" link>
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
    </v-card>
    <AlunosGrid />
  </v-layout>
</template>

<script>
import AlunosGrid from './AlunosGrid.vue';
export default {
  components: { AlunosGrid },
  name: 'AcademicModule',
  data: () => ({
    items: [
      { title: 'Alunos', icon: 'mdi-account-box' },
    ],
    right: null,
  }),
}
</script>
