<script setup lang="ts">
import { ref } from 'vue';
import Instruction from './components/Instruction.vue';

// Reactive list of items
const items = ref<{ id: number; name: string; isEditing: boolean }[]>([]);

// Add a new item
const addItem = () => {
  const newItem = { id: Date.now(), name: '', isEditing: true };
  items.value.push(newItem);
};

// Remove an item
const removeItem = (id: number) => {
  items.value = items.value.filter(item => item.id !== id);
};

// Save the edited item
const saveItem = (item: { id: number; name: string; isEditing: boolean }) => {
  item.isEditing = false;
};

// Edit an item
const editItem = (item: { id: number; name: string; isEditing: boolean }) => {
  item.isEditing = true;
};

// Get all existing names for uniqueness validation
const getAllNames = () => items.value.map(item => item.name);
</script>

<template>
  <header>
    <div class="top">
      Turing Machine
    </div>
  </header>
  <main>
    <div class="container">
      <div class="content">
        <button @click="addItem" class="add-button">Legg til tilstand</button>
        <ul class="item-list">
          <li v-for="item in items" :key="item.id" class="item">
            <Instruction
              v-model="item.name"
              :isEditing="item.isEditing"
              :allValues="getAllNames()"
              @save="saveItem(item)"
              @edit="editItem(item)"
            />
            <button @click="removeItem(item.id)" class="remove-button">X</button>
          </li>
        </ul>
      </div>
    </div>
    <div class="footer">
      <div class="footer-content">
        <p>© 2025 Bjosoft. All rights reserved.</p>
      </div>
    </div>
  </main>
</template>

<style scoped>
.container {
  padding: 20px;
}

.add-button {
  margin-bottom: 10px;
  padding: 8px 12px;
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.item-list {
  list-style: none;
  padding: 0;
}

.item {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.remove-button {
  padding: 6px 10px;
  background-color: #f44336;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
</style>