import AdvancedComponent from "../core/AdvancedComponent.js";
import makeFBXLoader from "../util/makeFBXLoader.js";

const loadFBX = makeFBXLoader(fileName => `assets/CampFire/${fileName}`);

export default position =>
  class Landscape extends AdvancedComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const mesh = (await loadFBX("camp_fire_model.fbx")).children[0];

      // Decrease specularity
      mesh.material.specular = new THREE.Color(0.2, 0.2, 0.2);

      // Scale down and move to right position
      mesh.applyMatrix(new THREE.Matrix4().makeScale(0.008, 0.008, 0.008));

      mesh.geometry.computeBoundingBox();

      mesh.translateX(position.x);
      mesh.translateY(-position.z + 2);
      mesh.translateZ(-mesh.geometry.boundingBox.min.z + position.y);

      this.createFire(mesh);

      // Add mesh to scene
      scene.add(mesh);
    }

    createFire(mesh) {
      var fireGeometry = new THREE.Geometry();

      for (var i = 0; i < 100; i++) {
        var fire = new THREE.Vector3();
        fire.x = THREE.Math.randFloatSpread(1);
        fire.y = THREE.Math.randFloatSpread(1);
        fire.z = THREE.Math.randFloatSpread(2);

        fireGeometry.vertices.push(fire);
      }

      var fireMaterial = new THREE.PointsMaterial({
        color: "orange",
        transparent: true,
        opacity: 0.75,
        size: 0.05
      });

      this.fire = new THREE.Points(fireGeometry, fireMaterial);

      mesh.add(this.fire);

      // create the particle system
      mesh.add(new THREE.PointLight("orange", 1, 100, 5));
    }

    update() {
      if (!this.fire) return;

      this.fire.geometry.vertices.forEach(vertice => {
        const displacementRatio = Math.abs(vertice.z * 0.00175) + 1;

        vertice.z += Math.abs(
          THREE.Math.randFloatSpread(0.015) * displacementRatio
        );
        vertice.x /= displacementRatio;
        vertice.y /= displacementRatio;

        if (vertice.z > 2) {
          vertice.x = THREE.Math.randFloatSpread(1);
          vertice.y = THREE.Math.randFloatSpread(1);
          vertice.z = 0;
        }
      });

      this.fire.geometry.verticesNeedUpdate = true;
    }
  };
