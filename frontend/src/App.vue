<template>
  <div id="app">
    <header v-if="isAuthenticated">
      <h1>Controle de Pessoas</h1>

      <nav class="navigation">
        <button class="button" @click="currentView = 'person'">Nova Pessoa</button>
        <button class="button" @click="currentView = 'list'">Lista de Pessoas</button>
        <button class="button logout" @click="logout"><i class="bi bi-box-arrow-right"></i></button>
      </nav>
    </header>

    <main>
      <LoginView v-if="!isAuthenticated" @logged-in="onLogin" />
      <PersonForm v-if="isAuthenticated && currentView === 'person'" :person="selectedPerson" @saved="onSaved" @cancel="onCancel" />
      <PersonList v-if="isAuthenticated && currentView === 'list'" :key="listKey" @edit-person="onEdit" />
    </main>
  </div>
</template>

<script>
import PersonForm from './components/PersonForm.vue'
import PersonList from './components/PersonList.vue'
import LoginView from './views/LoginView.vue';

export default {
  name: 'App',
  components: {
    PersonForm,
    PersonList,
    LoginView
  },
  data() {
    return {
      currentView: 'list',
      isAuthenticated: false,
      selectedPerson: null,
      listKey: 0
    }
  },
  mounted() {
    this.checkAuth();
  },
  methods: {
    checkAuth() {
      this.isAuthenticated = !!sessionStorage.getItem('token');
    },
    onLogin() {
      this.isAuthenticated = true
    },
    onEdit(person) {
      this.selectedPerson = person
      this.currentView = 'person'
    },
    onSaved() {
      this.selectedPerson = null
      this.currentView = 'list'
      this.listKey ++
    },
    onCancel() {
      this.selectedPerson = null
      this.currentView = 'list'
    },
    logout() {
      sessionStorage.removeItem('token');
      this.isAuthenticated = false;
      this.currentView = 'person';
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

input {
  padding: 8px;
  border: none;
  background-color: #494949;
  color: #ffffff;
}

button {
  all: unset;
  box-sizing: border-box;
  cursor: pointer;
  font-family: inherit;
}

.navigation {
  display: flex;
  gap: 10px;
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