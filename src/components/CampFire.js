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
      mesh.material.specular = new THREE.Color(0.1, 0.1, 0.1);

      // Scale down and move to right position
      mesh.applyMatrix(new THREE.Matrix4().makeScale(0.005, 0.005, 0.005));

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
        color: "#ffbb8a",
        transparent: true,
        opacity: 0.35,
        size: 0.05
      });

      this.fire = new THREE.Points(fireGeometry, fireMaterial);

      mesh.add(this.fire);

      this.light = new THREE.PointLight("#E38C40", 5, 50, 5);

      mesh.add(this.light);
    }

    update() {
      if (!this.fire) return;

      this.light.position.setX(3.5);
      this.light.position.setZ(-0.5);
      this.light.position.setY(12);

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
