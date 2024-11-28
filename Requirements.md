# Unity Game Project Requirements

## Hosting

- [ ] Host Unity game built using WebGL on a website service (e.g., GitHub Pages or Netlify).
  - [ ] Ensure the game runs without requiring server-side code on the hosting service.

## Database Integration

- [ ] Store user progress (e.g., levels, coins, points, enemies defeated, matches played/won) in a database.
- [ ] Retrieve user progress when the user logs back in to continue from where they left off.
  - [ ] Example: If the user reached level 3, the game resumes at level 3 when they log in again.

## API Integration

- [ ] Host an API on a web service supporting server-side code (e.g., Qoddi, Cyclic.sh, Render).
- [ ] Securely communicate with the database through API calls.
  - [ ] Use API for retrieving user progress.
  - [ ] Use API for saving user progress.

## Authentication

- [x] Implement login functionality with hashed passwords.
- [ ] Validate passwords:
  - [ ] Minimum 10 characters.
  - [ ] At least one uppercase letter.
  - [ ] At least one lowercase letter.
  - [ ] At least one number.
  - [ ] At least one symbol.
- [x] Use UnityWebRequest to send username and password to API for authentication.
- [ ] Ensure the user must be logged in to play the game.

## Authorization

- [ ] Restrict users to edit only their own game details.
- [ ] Return a `400 Bad Request` for any attempt to edit another user's data.

## Session Management

- [ ] Use encrypted MongoDB cookies for session management.

## Game Features

- [ ] Implement game collisions.
- [ ] Use prefabs for reusable game objects.
- [ ] Add sprite animations for dynamic visuals.
- [ ] Include particle effects for actions or events.
- [ ] Implement object instantiation and destruction using:
  - [ ] Death Barrier.
  - [ ] Other suitable methods to remove unused objects.

## Additional Features (Optional for Top Marks)

- [ ] Add enhancements or features beyond the listed requirements to improve the game.
