export async function getBoss({page = 1, pageSize = 10, sort, search, element}) {
  const params = new URLSearchParams();

  params.append('page', page);
  params.append('pageSize', pageSize);

  if (sort) params.append('sort', sort);
  if (search) params.append('search', search);
  if (element) params.append('element', element);

  const response = await fetch(`/api/hero?${params.toString()}`);

  if (!response.ok) {
    const jsonError = await response.json();
    if (jsonError.errors) {
      const messages = Object.values(jsonError.errors).flat();
      const error = new Error("Validation failed")
      error.messages = messages;
      throw error;
    }
  }
  
  return response.json();
}

export async function createBoss(formData) {
  const response = await fetch('/api/boss', {
    method: 'POST',
    body: formData
  });

  if (!response.ok) {
    const jsonError = await response.json();
    if (jsonError.errors) {
      const messages = Object.values(jsonError.errors).flat();
      const error = new Error("Validation failed")
      error.messages = messages;
      throw error;
    }
  }

  return response.json();
}

export async function updateBoss(guid, formData) {
  const response = await fetch(`/api/boss/${guid}`, {
    method: 'PUT',
    body: formData
  });

  if (!response.ok) {
    const jsonError = await response.json();
    if (jsonError.errors) {
      const messages = Object.values(jsonError.errors).flat();
      const error = new Error("Validation failed")
      error.messages = messages;
      throw error;
    }
  }

  return response.json();
}

export async function deleteBoss(guid) {
  const response = await fetch(`/api/boss/${guid}`, {
    method: 'DELETE'
  });

  if (!response.ok) {
    const jsonError = await response.json();
    if (jsonError.errors) {
      const messages = Object.values(jsonError.errors).flat();
      const error = new Error("Validation failed")
      error.messages = messages;
      throw error;
    }
  }
}