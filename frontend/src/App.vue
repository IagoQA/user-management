<template>
  <div id="app">
    <header>
      <h1>Controle de Pessoas</h1>

      <nav class="navigation">
        <button class="button" @click="currentView = 'user'">Novo Usuário</button>
        <button class="button" @click="currentView = 'person'">Nova Pessoa</button>
        <button class="button" @click="currentView = 'list'">Lista de Pessoas</button>
      </nav>
    </header>

    <main>
      <UserForm v-if="currentView === 'user'" />
      <PersonForm v-if="currentView === 'person'" />
      <PersonList v-if="currentView === 'list'" />
    </main>
  </div>
</template>

<script>
import UserForm from './components/UserForm.vue'
import PersonForm from './components/PersonForm.vue'
import PersonList from './components/PersonList.vue'

export default {
  name: 'App',
  components: {
    UserForm,
    PersonForm,
    PersonList
  },
  data() {
    return {
      currentView: 'user'
    }
  }
}
</script>

<style>
* {
  box-sizing: border-box;
  margin: 0;
}

#app {
  margin: 0;
  padding: 20px;
  font-family: Arial, Helvetica, sans-serif;
  background-color: #1b1a1b;
  color: #ffffff;
  min-height: 100vh;
}

main {
  margin-top: 30px;
}

header {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

button {
  all: unset;
  box-sizing: border-box;
  cursor: pointer;
  font-family: inherit;
}

.navigation .button {
  padding: 8px 14px;
  background-color: #2b2b2b;
  color: #ffffff;
  border: none;
  cursor: pointer;
}

.navigation .button:hover {
  background-color: #00d0d0;
  color: #000;
}
</style>