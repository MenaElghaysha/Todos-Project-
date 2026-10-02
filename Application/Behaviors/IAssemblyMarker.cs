namespace Application;


// طب ما هي interface فاضية؟ بتعمل إيه؟ 😂

// بالضبط، هي فاضية عمدًا.

// إحنا بنستخدمها كـ marker علشان نقدر نقول لـ MediatR وFluentValidation:

// روح للـ Application assembly ودور على الحاجات اللي محتاجها.

// دي معناها:

// هات الـ assembly اللي فيها IAssemblyMarker.

// وبما إن IAssemblyMarker موجودة داخل Application:

// Application.dll

// فإحنا قدرنا نحدد الـ Application assembly.
public interface IAssemblyMarker
{
    // This interface serves as a marker for assembly scanning and does not contain any members.
}