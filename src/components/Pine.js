import StaticComponent from "../core/StaticComponent.js";

const getRandomClamped = (min = 0.7, max = 1) =>
  Math.random() * (max - min) + min;

const PineFactory = position =>
  class extends StaticComponent {
    constructor(scene) {
      super();

      const tree = new THREE.Group();

      this.createLeaves(tree);
      this.createTrunk(tree);

      tree.position.add(position);

      tree.position.y += 1;

      scene.add(tree);
    }

    createTrunk(tree) {
      const geometry = new THREE.CylinderGeometry(0.25, 0.25, 1, 8, 2, true);
      const material = new THREE.MeshStandardMaterial({
        color: "#382C21",
        roughness: 1,
        metalness: 0.1,
        flatShading: true
      });

      const mesh = new THREE.Mesh(geometry, material);

      mesh.castShadow = true; //default is false
      mesh.receiveShadow = true; //default is false
      mesh.position.y -= 0.5;

      tree.add(mesh);
    }

    createLeaves(tree) {
      const segments = 5;

      const material = new THREE.MeshStandardMaterial({
        color: "#008040",
        roughness: 0.75,
        metalness: 0.3,
        flatShading: true
      });

      for (let index = 0; index < segments - 1; index++) {
        const width = (segments - index) / 3;

        const branch = new THREE.Mesh(
          new THREE.CylinderGeometry(0, width, 2, 6, 1),
          material
        );

        const rotationLimit = width / 25;

        branch.position.y += index + 1;
        branch.rotation.y += getRandomClamped(-0.25, 0.25);
        branch.rotation.z += getRandomClamped(-rotationLimit, rotationLimit);
        branch.rotation.x += getRandomClamped(-rotationLimit, rotationLimit);

        branch.castShadow = true; //default is false
        branch.receiveShadow = true; //default is false

        tree.add(branch);
      }

      tree.castShadow = true; //default is false
    }
  };

export default PineFactory;
